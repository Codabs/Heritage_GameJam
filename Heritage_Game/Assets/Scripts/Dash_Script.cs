using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD;
using DG.Tweening;

public class Dash_Script : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    public new Rigidbody2D rigidbody2D;
    public Movement_Script movement_Script;
    public float dashSpeed;
    public float dashCooldown;
    public ParticleSystem dashParticule;
    public bool canDash;
    public Vector2 lastDirection;
    public bool isDashing = false;
    public bool haveTheCrown = false;
    public int numberOfPlayer = 1;
    public Camera cam; 

    //=========
    //FONCTION
    //=========
    private void Start()
    {
        canDash = true;
    }
    public void DashingOnMouse(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        print(mousePosition);
        Vector2 direction = mousePosition - transform.position;

        Dash(direction.normalized);
    }
    public void DashingOnController(InputAction.CallbackContext context)
    {
        Dash(movement_Script.lastMovement);
    }
    public void Dash(Vector2 direction)
    {
        if (canDash && Win_Condition_Script.Instance.playerWhoHasTheCrown != numberOfPlayer && !movement_Script.isThePlayerStun)
        {
            isDashing = true;
            dashParticule.Play();
            float startMaxSpeed = movement_Script.maxSpeed;
            movement_Script.maxSpeed = dashSpeed;
            rigidbody2D.velocity = direction * dashSpeed;

            Sequence s = DOTween.Sequence();
            DOVirtual.Float(movement_Script.maxSpeed, startMaxSpeed, 
                1f, v => movement_Script.maxSpeed = v).OnComplete(() => isDashing = false) ;

            StartCoroutine(DashTimer());
        }
    }
    public IEnumerator DashTimer()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
