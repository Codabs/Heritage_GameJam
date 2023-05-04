using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movement_Script : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    public PlayerInput playerInput;
    public new Rigidbody2D rigidbody2D;
    public Dash_Script dashScript;
    public float speed = 1.2f;
    public float maxSpeed = 10;
    public Vector2 lastMovement = Vector2.zero;
    public Vector2 movement;
    public bool isThePlayerStun = false;
    public GameObject stunParticule;
    public Animator player_Animator;

    //=========
    //FONCTION
    //=========
    private void FixedUpdate()
    {
        //Clamp Velocity
        rigidbody2D.velocity = new
                (Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed, maxSpeed),
                Mathf.Clamp(rigidbody2D.velocity.y, -maxSpeed, maxSpeed));
        //Movement
        rigidbody2D.velocity += movement * speed;
        if (movement == Vector2.zero) return;
        lastMovement = movement;
    }
    public void XMovement(InputAction.CallbackContext context)
    {
        float movementInput = context.ReadValue<float>();
        movement.x = movementInput;
    }
    public void YMovement(InputAction.CallbackContext context)
    {
        float movementInput = context.ReadValue<float>();
        movement.y = movementInput;
    }
    public void Stun()
    {
        StartCoroutine(RemoveInputForXSecond(4f));
        gameObject.layer = 6;
        if (dashScript.numberOfPlayer == Win_Condition_Script.Instance.playerWhoHasTheCrown)
        {
            Win_Condition_Script.Instance.DropTheCrown(transform.position);
            dashScript.haveTheCrown = false;

            Sequence s = DOTween.Sequence();
            s.SetUpdate(true);
            DOVirtual.Float(0, 1,
                1f, v => Time.timeScale = v);
        }
    }
    public IEnumerator RemoveInputForXSecond(float secondOfStun)
    {
        float startSpeed = speed;
        speed = 0;
        isThePlayerStun = true;
        stunParticule.SetActive(true);
        player_Animator.Play("Fail");
        rigidbody2D.velocity = new(50, 50);
        yield return new WaitForSeconds(secondOfStun);
        isThePlayerStun = false;
        speed = startSpeed;
        stunParticule.SetActive(false);
        player_Animator.Play("Idle");
        gameObject.layer = 0;
    }
}
