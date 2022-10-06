using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private BoxCollider2D jumpPadColldier;
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip springAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            animator.SetTrigger("IsUsed");
            audioSource.PlayOneShot(springAudioClip);
            player.rb2D.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);     
        }
    }
}
