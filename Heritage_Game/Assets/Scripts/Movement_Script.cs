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
    public float maxSpeedMemory;
    public Vector2 lastMovement = Vector2.zero;
    public Vector2 movement;
    public bool isThePlayerStun = false;
    public GameObject stunParticule;
    public Animator player_Animator;

    public List<TrailRenderer> trailsList = new();
    public int currentTrail = 0;
    //=========
    //FONCTION
    //=========
    private void Awake()
    {
        maxSpeedMemory = maxSpeed;
    }
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
        player_Animator.Play("Running");
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Stun");
        StartCoroutine(RemoveInputForXSecond(4f));
        gameObject.layer = 6;
        if (dashScript.numberOfPlayer == Win_Condition_Script.Instance.playerWhoHasTheCrown)
        {
            Win_Condition_Script.Instance.DropTheCrown(transform.position);
            dashScript.haveTheCrown = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Wilhelm");


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
    public void ChangeTrail(int number)
    {
        foreach(TrailRenderer trail in trailsList)
        {
            trail.gameObject.SetActive(false);
        }
        trailsList[number].gameObject.SetActive(true);
        currentTrail = number;
    }
}
