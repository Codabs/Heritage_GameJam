using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/DashCollision");
        if (collision.gameObject.TryGetComponent<Movement_Script>(out Movement_Script otherPlayerScript))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/HitPlayer");
            if (dash_Script.isDashing && !otherPlayerScript.isThePlayerStun)
            {
                //Stun the other player
                otherPlayerScript.Stun();
                return;
            }
        }
    }
}
