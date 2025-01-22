using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    private float horizontalMove;
    [SerializeField] float speed;
    [SerializeField] float runRate;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    //private Animator playerAnimator;
    private bool canMove = true;
    //private bool isWalking;

    private float timer = 0f;
    [SerializeField] private float timeToRun;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            horizontalMove = Input.GetAxisRaw("Horizontal");

            if (horizontalMove != 0){
                //isWalking = true;
                //playerAnimator.SetBool("isWalking", true);
        
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
                //playerAnimator.SetBool("isWalking", false);
            }
        }
    }

    private void FixedUpdate() {
        float moveSpeed = Mathf.Lerp(speed, speed*runRate, timer/timeToRun);

        Vector2 movement = new(moveSpeed * horizontalMove, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
}
