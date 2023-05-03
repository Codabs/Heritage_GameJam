using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Script : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    public PlayerInput playerInput;
    public new Rigidbody2D rigidbody2D;
    public float speed = 1.2f;
    public float maxSpeed = 10;
    public Vector2 lastMovement = Vector2.zero;
    public Vector2 movement;
    public bool isThePlayerStun = false;
    public GameObject stunParticule;

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
    }
    public void XMovement(InputAction.CallbackContext context)
    {
        float movementInput = context.ReadValue<float>();
        movement.x = movementInput;
        if (movementInput != 0)
        lastMovement = new( movementInput, movementInput);
    }
    public void YMovement(InputAction.CallbackContext context)
    {
        float movementInput = context.ReadValue<float>();
        movement.y = movementInput;
        if (movementInput != 0)
            lastMovement = new(movementInput, movementInput);
    }
    public void Stun()
    {
        StartCoroutine(RemoveInputForXSecond(4f));
    }
    public IEnumerator RemoveInputForXSecond(float secondOfStun)
    {
        float startSpeed = speed;
        speed = 0;
        isThePlayerStun = true;
        stunParticule.SetActive(true);
        yield return new WaitForSeconds(secondOfStun);
        isThePlayerStun = false;
        speed = startSpeed;
        stunParticule.SetActive(false);
    }
}
