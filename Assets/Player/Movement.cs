using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    public GameObject ground;
    private TilemapCollider2D groundCollider;

    private Rigidbody2D rigidbody;
    private BoxCollider2D legsCollider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public void Start()
    {
        groundCollider = ground.GetComponent<TilemapCollider2D>();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        legsCollider = gameObject.GetComponentInChildren<BoxCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        Vector2 velocity = rigidbody.velocity;

        float moveInput = Input.GetAxis("Horizontal");
        bool isGrounded = legsCollider.IsTouching(groundCollider);

        if (moveInput == 0 && isGrounded)
        {
            velocity.x = 0;
            SetIsRunning(false);
        }
        else
        {
            bool isMovingLeft = moveInput < 0;

            velocity.x = isMovingLeft ? -moveSpeed : moveSpeed;
            spriteRenderer.flipX = isMovingLeft;

            SetIsRunning(true);
        }

        float jumpInput = Input.GetAxis("Jump");

        if (jumpInput > 0 && isGrounded)
        {
            velocity.y = jumpSpeed;
        }

        rigidbody.velocity = velocity;
    }

    private void SetIsRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }
}
