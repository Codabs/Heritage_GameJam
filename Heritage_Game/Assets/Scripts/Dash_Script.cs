using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD;
using DG.Tweening;
using Cinemachine;

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
    public ParticleSystem dashRechargeParticule;
    public bool canDash;
    public Vector2 lastDirection;
    public bool isDashing = false;
    public bool haveTheCrown = false;
    public int numberOfPlayer = 1;
    public Camera cam;
    public CinemachineImpulseSource impulseSource;
    public Animator player_Animator;

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
            movement_Script.maxSpeed = dashSpeed;
            rigidbody2D.velocity = direction * dashSpeed;

            Sequence s = DOTween.Sequence();
            DOVirtual.Float(movement_Script.maxSpeed, movement_Script.maxSpeedMemory, 
                1f, v => movement_Script.maxSpeed = v).OnComplete(() => isDashing = false) ;

            impulseSource.GenerateImpulse();

            StartCoroutine(DashTimer());
        }
    }
    public IEnumerator DashTimer()
    {
        canDash = false;
        dashRechargeParticule.Play();
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        player_Animator.gameObject.transform.DOPunchScale(Vector3.one * 1.2f, 0.3f);
    }
}
