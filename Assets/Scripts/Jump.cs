using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float buttonTime = 0.3f;
    [SerializeField] float cancelRate = 80f;
    private float jumpTime;
    private bool isJumping;
    private bool jumpCancelled;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    //private Animator playerAnimator;

    // changing the gravity on jumping
    // create a dinamic movement
    public float gravityScaleUp = 1f;
    public float gravityScaleDown = 2f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);

        if (IsGrounded()){
            //playerAnimator.SetBool("isJumping", false);

            if(jump && !isJumping){
                //playerAnimator.SetBool("isJumping", true);
                PerformJump();
            }
        }

        if (isJumping){
            jumpTime += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                isJumping = false;
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
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * (Physics2D.gravity.y * rb.gravityScale));
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
}