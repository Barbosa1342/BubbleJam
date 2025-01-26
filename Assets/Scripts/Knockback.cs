using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private bool onKnockback;
    public bool OnKnockback => onKnockback;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector3 enemyPosition, float knockbackForce){
        if(gameObject.activeSelf){
            StartCoroutine(CoroutineKnockback(enemyPosition, knockbackForce));
        }
    }

    private IEnumerator CoroutineKnockback(Vector3 enemyPosition, float knockbackForce){
        onKnockback = true;

        Vector2 direction = transform.position - enemyPosition;
        direction.y = Vector2.up.y;
        direction = direction.normalized;

        rb.linearVelocity = direction * knockbackForce;

        // i made this up
        float time = knockbackForce / 10f;

        yield return new WaitForSeconds(time);
    
        onKnockback = false;
    }
}
