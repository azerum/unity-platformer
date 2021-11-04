using UnityEngine;

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

    public float minHeightToShowJumpAnimation;

    public LayerMask groundLayer;

    private Rigidbody2D rigidbody;
    private BoxCollider2D legsCollider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private JumpingState jumpingState;

    public void Start()
    {
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
        bool isGrounded = legsCollider.IsTouchingLayers(groundLayer);
        
        switch (jumpingState)
        {
            case JumpingState.NotJumping:
                if (isGrounded)
                {
                    float jumpInput = Input.GetAxis("Jump");

                    if (jumpInput > 0.0f)
                    {
                        velocity.y = jumpInput * jumpSpeed;
                        jumpingState = JumpingState.FlyingUp;
                    }
                }
                else
                {
                    float? distanceAboveGround = CalculateDistanceAboveGround();

                    if (
                        distanceAboveGround == null ||
                        distanceAboveGround >= minHeightToShowJumpAnimation
                    )
                    {
                        jumpingState = JumpingState.FlyingUp;
                    }
                }
                break;

            case JumpingState.FlyingUp:
                if (velocity.y <= 0.0f)
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

    /// <summary>
    /// Returns a distance from the center of the player to the ground
    /// below him. If there is no ground below the player (for example,
    /// player is jumping over a pit), the function returns <c>null</c>
    /// </summary>
    private float? CalculateDistanceAboveGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            Mathf.Infinity,
            groundLayer
        );

        if (hit.collider == null)
        {
            return null;
        }

        return hit.distance;
    }
}
