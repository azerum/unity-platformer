using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    public float minHeightToShowJumpAnimation;

    public LayerMask groundLayer;

    private Rigidbody2D playerRigidbody;
    private BoxCollider2D legsCollider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    enum FlyingAnimationState
    {
        NotFlying,
        FlyingUp,
        Landing
    }

    private FlyingAnimationState flyingAnimationState;
    private bool isFrozen;

    public void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        legsCollider = gameObject.GetComponentInChildren<BoxCollider2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        flyingAnimationState = FlyingAnimationState.NotFlying;
        isFrozen = false;
    }

    public void FixedUpdate()
    {
        if (isFrozen)
        {
            return;
        }

        Vector2 velocity = playerRigidbody.velocity;

        HandleRunning(ref velocity);
        HandleJumping(ref velocity);

        playerRigidbody.velocity = velocity;
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
        switch (flyingAnimationState)
        {
            case FlyingAnimationState.NotFlying:
                if (IsGrounded())
                {
                    float jumpInput = Input.GetAxis("Jump");

                    if (jumpInput > 0.0f)
                    {
                        velocity.y = jumpInput * jumpSpeed;
                        flyingAnimationState = FlyingAnimationState.FlyingUp;
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
                        flyingAnimationState = FlyingAnimationState.FlyingUp;
                    }
                }
                break;

            case FlyingAnimationState.FlyingUp:
                if (velocity.y <= 0.0f)
                {
                    flyingAnimationState = FlyingAnimationState.Landing;
                }
                break;

            case FlyingAnimationState.Landing:
                if (IsGrounded())
                {
                    flyingAnimationState = FlyingAnimationState.NotFlying;
                }
                break;
        }

        animator.SetInteger("flyingState", (int)flyingAnimationState);
    }

    private bool IsGrounded()
    {
        return legsCollider.IsTouchingLayers(groundLayer);
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

    public void Freeze()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            animator.speed = 0.0f;
        }
    }

    public void Unfreeze()
    {
        if (isFrozen)
        {
            isFrozen = false;
            animator.speed = 1.0f;
        }
    }
}
