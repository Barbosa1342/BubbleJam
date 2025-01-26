using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    private void Update() {
        if(pauseScreen.activeSelf){
            if (Input.GetKeyDown(KeyCode.Escape)){
                Return();
            }    
        }else{
            if (Input.GetKeyDown(KeyCode.Escape)){
                Pause();
            }
        }
    }

    public void Quit(){
        MenuUtilities.QuitGame();
    }

    public void Pause(){
        MenuUtilities.PauseScene(pauseScreen);
    }

    public void Return(){
        MenuUtilities.UnpauseScene(pauseScreen);
    }

    public void Reset(){
        MenuUtilities.ResetScene();
    }
}
