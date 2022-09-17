using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float moveInput;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollectibleColor playerColor;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.velocity = new Vector2(moveInput * walkSpeed, rb2D.velocity.y);
    }

    private void OnMove(InputValue value) 
    {
        moveInput = value.Get<float>();
        Debug.Log($"Float of moveInput is {moveInput}");
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            CollectibleColor playerColor = collectible.color;

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
            Destroy(collectible.gameObject);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.LoadNextLevel();
        }
    }

}
