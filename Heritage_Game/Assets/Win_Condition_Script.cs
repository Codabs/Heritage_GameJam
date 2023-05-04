using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Win_Condition_Script : MonoBehaviour
{
    //========
    //VARIABLE
    //========
    public static Win_Condition_Script Instance;
    public int playerWhoHasTheCrown = 0;
    private float timerPlayerOne = 0;
    public TextMeshProUGUI textTimerOne;
    private float timerPlayerTwo = 0;
    public TextMeshProUGUI textTimerTwo;

    public float generalTimer = 60 * 5;
    public TextMeshProUGUI textTimerGeneral;

    public GameObject crownPrefab;

    public float TimerOne { 
        set {
            timerPlayerOne = value;
            textTimerOne.text = (Mathf.RoundToInt(value) * 100 / 60).ToString() + "%";
        } get { return timerPlayerOne; } }

    public float TimerTwo
    {
        set
        {
            timerPlayerTwo = value;
            textTimerTwo.text = (Mathf.RoundToInt(value) * 100 / 60).ToString() + "%";
        }
        get { return timerPlayerTwo; }
    }

    //========
    //FONCTION
    //========
    private void Awake()
    {
        Instance = this;
        TimerOne = 0;
        TimerTwo = 0;
    }
    private void Update()
    {
        switch(playerWhoHasTheCrown)
        {
            case 1:
                TimerOne += Time.deltaTime;
                break;
            case 2:
                TimerTwo += Time.deltaTime;
                break;
            default:
                break;
        }
        generalTimer -= Time.deltaTime;
        if ((int)generalTimer <= 0)
        {
            textTimerGeneral.text = "0";
            //End
        }
        else
        {
            textTimerGeneral.text = ((int)generalTimer).ToString();
        }

    }
    public void PlayerOneGetTheCrown()
    {
        playerWhoHasTheCrown = 1;
    }
    public void PlayerTwoGetTheCrown()
    {
        playerWhoHasTheCrown = 2;
    }
    public void DropTheCrown(Vector3 position)
    {
        GameObject crown = Instantiate(crownPrefab, position, Quaternion.identity);
        playerWhoHasTheCrown = 0;
        crown.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10;
    }
}
