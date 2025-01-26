using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] float speed;
    [SerializeField] Transform player;
    float lastPos;
    
    private void Start() {
        lastPos = player.position.x;
    }

    private void Update() {
        float change = player.position.x - lastPos;
        if (change > 0.1f){
            BackgroundMove(0.5f);
            lastPos = player.position.x;
        }else if (change < -0.1f){
            BackgroundMove(-0.5f);
            lastPos = player.position.x;
        }else{
            BackgroundMove(0);
        }
    }

    public void BackgroundMove(float movement){
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime * movement, transform.position.y, 0);

        if (transform.localPosition.x >= background.preferredWidth * 2){
            transform.localPosition = new Vector3(transform.localPosition.x - background.preferredWidth * 3, 0, 0);
        }else if (transform.localPosition.x <= -background.preferredWidth){
            transform.localPosition = new Vector3(transform.localPosition.x + background.preferredWidth * 3, 0, 0);
        }
    }
}
