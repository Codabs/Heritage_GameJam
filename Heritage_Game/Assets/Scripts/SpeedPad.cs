using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    
    //=========
    //FONCTION
    //=========
   
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            print("on speedpad");

            rigidbody.velocity += new Vector2 (transform.forward.x,transform.forward.y)*50;


        }
    }

}