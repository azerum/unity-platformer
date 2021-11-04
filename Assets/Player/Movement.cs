using UnityEngine;
using UnityEngine.Tilemaps;

public enum JumpingState
{
    NotJumping,
    FlyingUp,
    Landing
}

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

    private JumpingState jumpingState;

    public void Start()
    { 
        groundCollider = ground.GetComponent<TilemapCollider2D>();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        legsCollider = gameObject.GetComponentInChildren<BoxCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        jumpingState = JumpingState.NotJumping;
    }

    public void FixedUpdate()
    {
        Vector2 velocity = rigidbody.velocity;

        HandleRunning(ref velocity);
        HandleJumping(ref velocity);

        rigidbody.velocity = velocity;
    }

    private void HandleRunning(ref Vector2 velocity)
    {
        float moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * moveSpeed;

        animator.SetBool("isRunning", velocity.x != 0.0f);

        if (velocity.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocity.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleJumping(ref Vector2 velocity)
    {
        float jumpInput = Input.GetAxis("Jump");

        float legsGroundDistance = legsCollider.Distance(groundCollider).distance;
        bool isGrounded = legsGroundDistance <= 0.0f;

        switch (jumpingState)
        {
            case JumpingState.NotJumping:
                if (isGrounded && jumpInput > 0.0f)
                {
                    velocity.y = jumpInput * jumpSpeed;
                    jumpingState = JumpingState.FlyingUp;
                }
                else if (legsGroundDistance >= 1.0f)
                {
                    jumpingState = JumpingState.FlyingUp;
                }
                break;

            case JumpingState.FlyingUp:
                if (!isGrounded && velocity.y <= 0.0f)
                {
                    jumpingState = JumpingState.Landing;
                }
                break;

            case JumpingState.Landing:
                if (isGrounded)
                {
                    jumpingState = JumpingState.NotJumping;
                }
                break;
        }
        
        animator.SetInteger("jumpingState", (int)jumpingState);
    }
}
