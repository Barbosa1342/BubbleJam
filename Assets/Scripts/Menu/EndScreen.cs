using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    
    private void OnTriggerEnter2D(Collider2D other) {
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
