using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void StartGame(){
        MenuUtilities.GoToScene(sceneName);
    }

    public void QuitGame(){
        MenuUtilities.QuitGame();
    }
}
