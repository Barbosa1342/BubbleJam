using System.Collections;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] float timeToLive;

    private void Start() {
        StartCoroutine(PopBubble());
    }
    private void OnTriggerEnter2D(Collider2D colisor) {
        LayerMask groundLayer = LayerMask.NameToLayer("ground");
        
        if (colisor.CompareTag("Enemy")){
            //colisor.gameObject.GetComponent<healthSystem>().ChangeLife(1);
        }
        else if (colisor.gameObject.layer == groundLayer){
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