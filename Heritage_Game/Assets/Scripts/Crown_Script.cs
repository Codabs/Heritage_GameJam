using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Crown_Script : MonoBehaviour
{
    //========
    //VARIABLE
    //========
    public new Collider2D collider2D;
    public GameObject collectParticule;

    //========
    //FONCTION
    //========
    private void Awake()
    {
        GameObject.FindAnyObjectByType<CinemachineTargetGroup>().AddMember(transform, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Dash_Script>(out Dash_Script playerScript))
        {
            if (collision.gameObject.TryGetComponent<Movement_Script>(out Movement_Script movement_Script))
            {
                if (movement_Script.isThePlayerStun) return;
            }
            if(playerScript.numberOfPlayer == 1)
            {
                Win_Condition_Script.Instance.PlayerOneGetTheCrown();
                Instantiate(collectParticule, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Win_Condition_Script.Instance.PlayerTwoGetTheCrown();
                Instantiate(collectParticule, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(3f);
        gameObject.layer = 7;
    }
}
