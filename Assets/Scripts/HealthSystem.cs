using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;
    private int currentHealthPoints;

    public int MaxHealthPoints => maxHealthPoints;
    public int CurrentHealthPoints => currentHealthPoints;

    bool isDamageble;
    public bool IsDamageble => isDamageble;

    private void OnEnable() {
        isDamageble = false;
        currentHealthPoints = maxHealthPoints;
    }

    private void Update() {
        if (!isDamageble){
            StartCoroutine(TurnDamageble());
        }
    }

    public void ChangeHealth(int amount){
        if (isDamageble){
            float oldHealth = currentHealthPoints;
            currentHealthPoints += amount;

            currentHealthPoints = Mathf.Clamp(currentHealthPoints, 0, maxHealthPoints);
            isDamageble = false;
        }

        if(currentHealthPoints <= 0){
            Debug.Log("morreu");
            // Morre
        }
    }

    IEnumerator TurnDamageble(){
        yield return new WaitForSeconds(1f);
        isDamageble = true;
    }
}
