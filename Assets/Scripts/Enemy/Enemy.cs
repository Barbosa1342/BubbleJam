using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float visionRadio;
    [SerializeField] protected float walkSpeed;
    protected bool patrolling;

    [SerializeField] protected Transform player;
    protected Rigidbody2D rb;
    protected Animator rabbitAnimator;

    protected SpriteRenderer sprite;

    protected Knockback knockbackScript;
    [SerializeField] float knockbackForce;
    
    protected void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected void Start() {
        rabbitAnimator = GetComponent<Animator>();
        knockbackScript = GetComponent<Knockback>();
        patrolling = false;
    }
    
    virtual protected void FollowPlayer(){
        Vector2 direction = player.position - transform.position;
        direction = direction.normalized;
        direction.y = 0;

        rb.linearVelocity = walkSpeed * direction;

        if (rb.linearVelocityX >= 0.01f){
            sprite.flipX = true;
        }else if (rb.linearVelocityX <= -0.01f){
            sprite.flipX = false;
        }
    }

    virtual protected bool DetectPlayer(){
        Vector2 direction = player.position - transform.position;

        if (Mathf.Abs(direction.x) < visionRadio){
            return true;
        }
        return false;
    }

    virtual protected void StopWalking(){
        rb.linearVelocity = Vector2.zero;
    }

    virtual protected IEnumerator Patrol(){
        patrolling = true;
        
        int random = Random.Range(0, 3);

        Vector2 direction;
        if (random == 0){
            direction = Vector2.left;
            sprite.flipX = false;
        }else if(random == 1){
            direction = Vector2.right;
            sprite.flipX = true;
        }else{
            rabbitAnimator.SetBool("isPatrolling", false);
            direction = Vector2.zero;
        }

        rb.linearVelocity = 0.5f * walkSpeed * direction;

        yield return new WaitForSeconds(0.5f);

        StopWalking();
        rabbitAnimator.SetBool("isPatrolling", false);

        yield return new WaitForSeconds(1f);

        patrolling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")){
            player.GetComponent<HealthSystem>().ChangeHealth(-1);
            player.GetComponent<Knockback>().ApplyKnockback(transform.position, knockbackForce);
        }
    }
}
