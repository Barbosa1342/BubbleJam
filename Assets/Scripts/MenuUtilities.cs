using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUtilities : MonoBehaviour
{
    static public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    static public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    static public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static public void PauseScene(GameObject pauseScreen){
        pauseScreen.SetActive(true);        
        Time.timeScale = 0;
    }

    static public void UnpauseScene(GameObject pauseScreen){
        pauseScreen.SetActive(false);        
        Time.timeScale = 1;
    }
}
