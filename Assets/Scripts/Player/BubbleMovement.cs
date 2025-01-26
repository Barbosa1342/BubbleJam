using System.Collections;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] float timeToLive;
    [SerializeField] float knockbackForce;

    private void Start() {
        StartCoroutine(PopBubble());
    }
    private void OnTriggerEnter2D(Collider2D colisor) {
        if(!colisor.CompareTag("Player")){
            LayerMask groundLayer = LayerMask.NameToLayer("ground");
            
            if (colisor.CompareTag("Enemy")){
                colisor.gameObject.GetComponent<HealthSystem>().ChangeHealth(-1);
                colisor.gameObject.GetComponent<Knockback>().ApplyKnockback(transform.position, knockbackForce);
            }
            else if (colisor.gameObject.layer == groundLayer){
                //
            }
            gameObject.SetActive(false);
        }
        
    }

    private void OnDisable() {
        // add pop Animation
    }

    IEnumerator PopBubble(){
        yield return new WaitForSeconds(timeToLive);

        gameObject.SetActive(false);
    }
}