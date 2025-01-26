using UnityEngine;
using UnityEngine.InputSystem;

public class HorizontalMove : MonoBehaviour
{
    InputAction moveAction;
    private float horizontalMove;
    private float crouchMove;

    [SerializeField] float speed;
    [SerializeField] float runRate;
    private float timer = 0f;
    [SerializeField] private float timeToRun;

    private bool canMove = true;
    private bool isCrouching = false;
    //private bool isWalking;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator playerAnimator;
    private GameObject standCollider;
    private GameObject crouchCollider;
    private Jump jumpScript;
    private Knockback knockbackScript;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        knockbackScript = GetComponent<Knockback>();
    }

    private void Start() {
        moveAction = InputSystem.actions.FindAction("Move");

        jumpScript = GetComponent<Jump>();

        standCollider = transform.Find("standingCollider")?.gameObject;
        crouchCollider = transform.Find("crouchingCollider")?.gameObject;

        if (standCollider == null || crouchCollider == null)
        {
            Debug.LogError("Stand Collider or Crouch Collider not found!");
        }

        SetCrouching(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Vector2 move = moveAction.ReadValue<Vector2>();
            horizontalMove = move.x;
            crouchMove = move.y;

            if (horizontalMove != 0){
                //isWalking = true;
                playerAnimator.SetBool("isWalking", true);
        
                if (horizontalMove > 0){
                    sprite.flipX = false;
                }else{
                    sprite.flipX = true;
                }

                timer += Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, timeToRun);
            }
            else{
                timer = 0f;
                //isWalking = false;
                playerAnimator.SetBool("isWalking", false);
            }

            if(crouchMove < 0 && jumpScript.IsGrounded()){
                SetCrouching(true);
            }else{
                SetCrouching(false);
            }
        }else{
            playerAnimator.SetBool("isWalking", false);
            horizontalMove = 0;
            crouchMove = 0;
        }
    }

    private void FixedUpdate() {
        if(!knockbackScript.OnKnockback){
            float moveSpeed = Mathf.Lerp(speed, speed*runRate, timer/timeToRun);

            if (isCrouching){
                moveSpeed *= 0.5f;
            }

            Vector2 movement = new(moveSpeed * horizontalMove, rb.linearVelocity.y);
            rb.linearVelocity = movement;
        }
    }

    public void SetCrouching(bool crouch)
    {
        isCrouching = crouch;

        playerAnimator.SetBool("isCrouching", crouch);
        crouchCollider.SetActive(crouch);
        standCollider.SetActive(!crouch);
    }

    public bool IsFacedRight(){
        return !sprite.flipX;
    }
}
