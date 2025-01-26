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

    private void Start() {
        attackAction = InputSystem.actions.FindAction("Attack");
        canShoot = true;
        horizontalMove = GetComponent<HorizontalMove>();
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
        //anim.SetBool("atirando", true);

        if (horizontalMove.IsFacedRight()){
            direcao = Vector2.right;
        }else{
            direcao = Vector2.left;
        };


        direcao.y = 0;
        direcao = direcao.normalized;

        Transform bala = Instantiate(soapBubblePrefab, transform.position, transform.rotation);
        bala.GetComponent<Rigidbody2D>().AddForce(direcao*bubbleSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        canShoot = true;
        //anim.SetBool("atirando", false);
    }
}
