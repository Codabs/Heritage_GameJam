using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget_Script : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    private void Update()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            targetGroup.AddMember(player.transform, 1, 1);
        }
    }
}
