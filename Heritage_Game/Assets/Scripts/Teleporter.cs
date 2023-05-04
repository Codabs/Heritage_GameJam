using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    public int code;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach(Teleporter tp in FindObjectsOfType<Teleporter>())
            {
                if (tp.code == code);
                {
                    Vector3 position = tp.gameObject.transform.position;
                    //position.y += 2;
                    other.gameObject.transform.position = position;
                }
            }
        }
    }
}