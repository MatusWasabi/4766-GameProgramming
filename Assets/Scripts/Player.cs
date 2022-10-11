using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Player : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float moveInput;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private bool doubleJumpUsed;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] private Vector2 velocity;

    [Header("Graphic")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollectibleColor playerColor;
    [SerializeField] private Animator animator;
    private static readonly int xVelocity = Animator.StringToHash("xVelocity");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    [Header("Audio")]
    [SerializeField] private PlayerAudioController playerAudioController;
    [SerializeField] private AudioClip jumpAudioClips;
    [SerializeField] private AudioClip fallAudioClips;
    [SerializeField] private AudioClip winAudioClips;
    [SerializeField] private AudioClip deadAudioClips;

    [Header("Particles")]
    [SerializeField] private GameObject footStepParticle;
    [SerializeField] private ParticleSystem groundDustParticle;
    [SerializeField] private ParticleSystem deadParticle;
    [SerializeField] private ParticleSystem winParticle;

    private void Awake()
    {
        doubleJumpUsed = true;
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(moveInput * walkSpeed, rb2D.velocity.y);
        velocity = rb2D.velocity;

        if (Grounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        animator.SetBool(IsGrounded, Grounded());
        animator.SetFloat(xVelocity, Mathf.Abs(moveInput));
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
        FlipPlayerSpirte();
        if (moveInput != 0) { footStepParticle.SetActive(true); }
        else { footStepParticle.SetActive(false); }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && coyoteTimeCounter > 0)
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0;
            playerAudioController.PlaySound(jumpAudioClips);
        }
        else if (value.isPressed && !doubleJumpUsed)
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJumpUsed = true;
            playerAudioController.PlaySound(jumpAudioClips);
        }
    }

    private void OnQuit(InputValue value)
    {
        if (value.isPressed)
        {
            GameManager.instance.LoadMainMenu();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            CollectibleColor playerColor = collectible.color;
            powerUp = collectible.powerUp;

            if (collectible.collectedSound != null)
            {
                playerAudioController.PlaySound(collectible.collectedSound);
            }

            switch (playerColor)
            {
                case CollectibleColor.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case CollectibleColor.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case CollectibleColor.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
            }

            switch (powerUp)
            {
                case PowerUp.DoubleJump:
                    doubleJumpUsed = false;
                    break;

                default:

                    break;

            }
            //collectible.gameObject.SetActive(false);
        }

        if (collision.gameObject.TryGetComponent(out FinishLine finishLine))
        {
            playerAudioController.PlaySound(winAudioClips);
            StartCoroutine(LoadPlayerWin(finishLine.LevelLoading));
            
        }

        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            deadParticle.Play();
            StartCoroutine(LoadPlayerDead());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAudioController.PlaySound(fallAudioClips);
            groundDustParticle.Play();
        }
    }

    private void FlipPlayerSpirte()
    {
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private bool Grounded()
    {
        float extendedHeight = 0.1f;
        Bounds boxColliderBound = boxCollider2D.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderBound.center,
            boxColliderBound.size, 0f, Vector2.down, extendedHeight, groundLayer);

        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
            
        }
        else
        {
            rayColor = Color.red;
            footStepParticle.SetActive(false);
        }
        return raycastHit2D.collider != null;
    }

    private void TakeDamage()
    {
        GameManager.instance.PlayerDeath();
    }


    private IEnumerator LoadPlayerDead()
    {
        playerAudioController.PlaySound(deadAudioClips);
        yield return new WaitForSeconds(1);
        TakeDamage();
    }

    private IEnumerator LoadPlayerWin(int level)
    {
        winParticle.Play();
        yield return new WaitForSeconds(1);
        GameManager.instance.LoadLevel(level);
    }




}
