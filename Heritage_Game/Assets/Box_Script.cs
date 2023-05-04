using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Script : MonoBehaviour
{
    //========
    //VARIABLE
    //========
    public List<Sprite> sprites = new();
    public int lifePoint = 3;
    public ParticleSystem breakParticule;
    public SpriteRenderer spriteComponent;
    //========
    //FONCTION
    //========
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Dash_Script>(out Dash_Script otherPlayerScript))
        {
            if (otherPlayerScript.isDashing)
            {
                breakParticule.Play();
                DestroyTheBox();
                return;
            }
            lifePoint--;
            breakParticule.Play();
            UpdateBox();
        }
    }
    public void DestroyTheBox()
    {
        Destroy(gameObject);
    }
    public void UpdateBox()
    {
        if(lifePoint <= 0)
        {
            DestroyTheBox();
        }
        else
        {
            spriteComponent.sprite = sprites[lifePoint];
        }
    }
}
