using System.Collections;
using UnityEngine;

public class RabbitGhost : Enemy
{
    private bool jumping = false;
    
    void FixedUpdate()
    {   
        if(knockbackScript.OnKnockback){
           StopAllCoroutines();
           patrolling = false;
           jumping = false;
        }else{
            if (DetectPlayer()){
                if (patrolling){
                    StopCoroutine(Patrol());
                    patrolling = false;
                }

                if(!jumping){
                    StartCoroutine(Jump());
                }
            }else{
                if(jumping){
                    StopCoroutine(Jump());
                    jumping = false;
                }

                if(!patrolling){
                    StartCoroutine(Patrol());
                }
            }
        }
        
        
    }

    private IEnumerator Jump(){
        jumping = true;

        Vector2 direction = player.position - transform.position;
        direction = direction.normalized;
        direction.y = 0;

        rb.linearVelocity = walkSpeed * direction;

        if (rb.linearVelocityX >= 0.01f){
            sprite.flipX = false;
        }else if (rb.linearVelocityX <= -0.01f){
            sprite.flipX = true;
        }

        yield return new WaitForSeconds(2f);
        StopWalking();
        yield return new WaitForSeconds(1f);
        jumping = false;
    }
}
