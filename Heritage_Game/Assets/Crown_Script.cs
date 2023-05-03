using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown_Script : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Movement_Script>(out Movement_Script playerScript))
        {

        }
    }
}
