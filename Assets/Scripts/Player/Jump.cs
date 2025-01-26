using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    InputAction jumpAction;
    private Rigidbody2D rb;
    [SerializeField] float buttonTime = 0.3f;
    [SerializeField] float cancelRate = 80f;
    private float jumpTime;
    private bool isJumping;
    private bool jumpCancelled;
    private bool canJump;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Animator playerAnimator;

    // changing the gravity on jumping
    // create a dinamic movement
    public float gravityScaleUp = 1f;
    public float gravityScaleDown = 2f;

    private void Start() {
        jumpAction = InputSystem.actions.FindAction("Jump");
        canJump = true;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jump = jumpAction.IsPressed();

        if (IsGrounded()){
            playerAnimator.SetBool("isJumping", false);

            if(jump && !isJumping && canJump){
                PerformJump();
            }
        }else{
            playerAnimator.SetBool("isJumping", true);
        }

        if (isJumping){
            jumpTime += Time.deltaTime;

            if (!jump)
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                isJumping = false;
                StartCoroutine(CanJump());
            }
        }

        if(rb.linearVelocity.y >= -0.1f)
        {
            rb.gravityScale = gravityScaleUp;
        }
        else if(rb.linearVelocity.y < -0.1f)
        {
            rb.gravityScale = gravityScaleDown;
        }

    }

    private void FixedUpdate()
    {
        if (jumpCancelled && isJumping && rb.linearVelocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private void PerformJump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        isJumping = true;
        jumpCancelled = false;
        jumpTime = 0f;
    }

    public bool IsGrounded(){
        if (rb.linearVelocity.y < 0.1f && rb.linearVelocity.y > -0.1f){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, groundLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator CanJump(){
        canJump = false;
        yield return new WaitForSeconds(0.3f);
        canJump = true;
    }
}