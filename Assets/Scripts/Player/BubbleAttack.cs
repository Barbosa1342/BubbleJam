using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class BubbleAttack : MonoBehaviour
{
    [SerializeField] float bubbleSpeed;
    [SerializeField] Transform soapBubblePrefab;
    private bool canShoot;
    private HorizontalMove horizontalMove;
    InputAction attackAction;

    private Animator playerAnimator;


    private void Start() {
        attackAction = InputSystem.actions.FindAction("Attack");
        canShoot = true;
        horizontalMove = GetComponent<HorizontalMove>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if(attackAction.IsPressed() && canShoot){
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        Vector2 direcao;
        canShoot = false;
        playerAnimator.SetBool("isAttacking", true);

        if (horizontalMove.IsFacedRight()){
            direcao = Vector2.right;
        }else{
            direcao = Vector2.left;
        };

        direcao.y = 0;
        direcao = direcao.normalized;

        yield return new WaitForSeconds(0.2f);

        Transform bala = Instantiate(soapBubblePrefab, transform.position, transform.rotation);
        bala.GetComponent<Rigidbody2D>().AddForce(direcao*bubbleSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.4f);

        canShoot = true;
        playerAnimator.SetBool("isAttacking", false);
    }
}
