using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]private HealthSystem healthSystemScript;
    [SerializeField] private Image[] healthContainer;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private float oldHealth;

    private void Start() {
        oldHealth = healthSystemScript.CurrentHealthPoints;
    }

    private void Update() {
        if (oldHealth != healthSystemScript.CurrentHealthPoints){
            updateHeart();
            oldHealth = healthSystemScript.CurrentHealthPoints;
        }
    }

    public void updateHeart(){
        for(int i = 1; i <= healthContainer.Length; i++){
            if( i <= healthSystemScript.CurrentHealthPoints){
                healthContainer[i-1].sprite = fullHeart;
            }else{
                healthContainer[i-1].sprite = emptyHeart;
            }
        }
    }
}
