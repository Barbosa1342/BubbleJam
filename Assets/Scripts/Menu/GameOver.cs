using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystemScript;
    [SerializeField] private GameObject gameOverScreen;

    private void Start() {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystemScript.CurrentHealthPoints <= 0){
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
