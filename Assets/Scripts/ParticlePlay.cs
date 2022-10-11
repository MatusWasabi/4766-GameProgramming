using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem thisParticle;

    private void Awake()
    {
        thisParticle = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        thisParticle.Play();
    }
}
