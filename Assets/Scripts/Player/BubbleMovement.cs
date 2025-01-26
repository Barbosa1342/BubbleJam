using System.Collections;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] float timeToLive;
    [SerializeField] float knockbackForce;
    [SerializeField] Animator bubbleAnimator;

    private Rigidbody2D rb;

    private void Start() {
        bubbleAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PopBubble());
    }
    private void OnTriggerEnter2D(Collider2D colisor) {
        if(!colisor.CompareTag("Player")){
            if (colisor.CompareTag("Enemy")){
                colisor.gameObject.GetComponent<HealthSystem>().ChangeHealth(-1);
                colisor.gameObject.GetComponent<Knockback>().ApplyKnockback(transform.position, knockbackForce);
            }
            StartCoroutine(PopAnimation());
        }
        
    }


    IEnumerator PopAnimation(){
        bubbleAnimator.SetBool("isExploding", true);
        rb.linearVelocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    IEnumerator PopBubble(){
        yield return new WaitForSeconds(timeToLive - 0.3f);
        StartCoroutine(PopAnimation());
    }
}