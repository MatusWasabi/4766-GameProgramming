using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private float moveInput;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollectibleColor playerColor;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool doubleJumpUsed;
    [SerializeField] private PowerUp powerUp;

    [SerializeField] private Animator animator;
    private static readonly int xVelocity = Animator.StringToHash("xVelocity");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");


    private void Awake()
    {
          doubleJumpUsed = true;
    }


    void FixedUpdate()
    {
        rb2D.velocity = new Vector2(moveInput * walkSpeed, rb2D.velocity.y);

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
        Debug.Log($"Float of moveInput is {moveInput}");
        FlipPlayerSpirte();

    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && coyoteTimeCounter > 0 )
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0;
        }
        else if (value.isPressed && !doubleJumpUsed)
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJumpUsed = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            CollectibleColor playerColor = collectible.color;
            powerUp = collectible.powerUp;

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
                    Debug.Log($"There is no power up");
                    break;

            }
             //collectible.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.LoadNextLevel();
        }

        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            Debug.Log($"The player is touching");
            TakeDamage();
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
            boxColliderBound.size, 0f,Vector2.down ,extendedHeight, groundLayer);
        
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        
        return raycastHit2D.collider != null;
    }

    private void TakeDamage()
    {
        GameManager.instance.ProcessPlayerDeath();
    }
}
