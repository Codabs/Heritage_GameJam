using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Movement_Script movement_Script;
    //=========
    //MONEBEHAVIOUR
    //=========
    private void Update()
    {
        //Get Input
        Vector2 input = movement_Script.lastMovement;


        //Rotate PLayer to Input
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, input);

        //Smooth Rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
    }
}
