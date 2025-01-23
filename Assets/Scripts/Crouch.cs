using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public bool isCrouching = false;

    private GameObject standCollider;
    private GameObject crouchCollider;

    //private Attack attackScript;

    void Start()
    {
        standCollider = transform.Find("standingCollider")?.gameObject;
        crouchCollider = transform.Find("crouchingCollider")?.gameObject;

        if (standCollider == null || crouchCollider == null)
        {
            Debug.LogError("Stand Collider or Crouch Collider not found!");
        }

        crouchCollider.SetActive(false);

        //attackScript = GetComponent<Attack>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S) && !attackScript.isAttacking)
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetCrouching(true);
        }
        if (!Input.GetKey(KeyCode.S))
        {
            SetCrouching(false);
        }
    }

    public void SetCrouching(bool crouch)
    {
        isCrouching = crouch;

        crouchCollider.SetActive(crouch);
        standCollider.SetActive(!crouch);
    }
}