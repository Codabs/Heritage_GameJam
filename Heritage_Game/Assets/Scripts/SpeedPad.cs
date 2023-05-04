using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpeedPad : MonoBehaviour
{
    
    //=========
    //FONCTION
    //=========
   
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            print("Player On Speed Pad");
            Movement_Script movement_Script = other.GetComponent<Movement_Script>();
            movement_Script.maxSpeed = 50;
            rigidbody2D.velocity += new Vector2(transform.forward.x, transform.forward.y) * 50;

            Sequence s = DOTween.Sequence();
            DOVirtual.Float(movement_Script.maxSpeed, movement_Script.maxSpeedMemory,
                1f, v => movement_Script.maxSpeed = v);
            StartCoroutine(ChangeTrailForXSecond(1, other.gameObject));
            other.GetComponent<Dash_Script>().dashParticule.Play();
        }
    }
    public IEnumerator ChangeTrailForXSecond(float second, GameObject player)
    {
        int previousTrail = player.GetComponent<Movement_Script>().currentTrail;
        player.GetComponent<Movement_Script>().ChangeTrail(1);
        yield return new WaitForSeconds(second);
        player.GetComponent<Movement_Script>().ChangeTrail(previousTrail);
    }
}