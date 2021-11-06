using System.Collections;
using UnityEngine;

public enum JumpingState
{
    NotJumping,
    FlyingUp,
    Landing
}

public class Movement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    public float moveSpeed;
    public float jumpSpeed;

    public float minHeightToShowJumpAnimation;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private JumpingState jumpingState;

    public LayerMask groundLayer;
    private BoxCollider2D legsCollider;

    public float knockbackRadius;
    public float knockbackTimestepsCount;

    private bool ignoreInput;
    private Coroutine knockbackCoroutine;

    public void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        jumpingState = JumpingState.NotJumping;

        legsCollider = gameObject.GetComponentInChildren<BoxCollider2D>();

        ignoreInput = false;
        knockbackCoroutine = null;
    }

    public void FixedUpdate()
    {
        if (ignoreInput)
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
        switch (jumpingState)
        {
            case JumpingState.NotJumping:
                if (IsGrounded())
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
                if (IsGrounded())
                {
                    jumpingState = JumpingState.NotJumping;
                }
                break;
        }

        animator.SetInteger("jumpingState", (int)jumpingState);
    }

    bool IsGrounded()
    {
        return legsCollider.IsTouchingLayers(groundLayer);
    }

    /// <summary>
    /// Returns a distance from the center of the player to the ground
    /// below him. If there is no ground below the player (for example,
    /// player is jumping over a pit), the function returns <c>null</c>
    /// </summary>
    float? CalculateDistanceAboveGround()
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

    public void Knockback(in Vector2 direction)
    {
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
        }

        knockbackCoroutine = StartCoroutine(DoKnockback(direction));
    }

    private IEnumerator DoKnockback(Vector2 direction)
    {
        ignoreInput = true;

        //S = v_i * t + (1/2)at^2
        //a = 2 * (S - (v_i * t)) / t^2

        //Consider v_i to be 0
        playerRigidbody.velocity = Vector2.zero;

        float duration = knockbackTimestepsCount * Time.fixedDeltaTime;
        float acceleration = 2 * knockbackRadius / (duration * duration);

        float speedChangeStep = acceleration * Time.fixedDeltaTime;
        Vector2 velocityChangeStep = direction * speedChangeStep;

        for (int i = 0; i < knockbackTimestepsCount; ++i)
        {
            playerRigidbody.velocity += velocityChangeStep;
            yield return new WaitForFixedUpdate();
        }

        playerRigidbody.velocity = Vector2.zero;
        ignoreInput = false;
    }
}
