using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider_Script : MonoBehaviour
{
    //========
    //VARIABLE
    //========
    public Dash_Script dash_Script;
    public Movement_Script movement_Script;
    public new Rigidbody2D rigidbody2D;
    public float speedNeededToKO = 30f;
    //========
    //Fonction
    //========
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Movement_Script>(out Movement_Script otherPlayerScript))
        {
            if(dash_Script.isDashing && !otherPlayerScript.isThePlayerStun)
            {
                //Stun the other player
                otherPlayerScript.Stun();
                return;
            }
            Debug.Log("No Stun");
        }
    }
}
