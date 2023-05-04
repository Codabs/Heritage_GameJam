using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

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

    public GameObject winning_Canvas;
    public TextMeshProUGUI textWinning;

    public GameObject crownPlayerOne;
    public GameObject crownPlayerTwo;

    public Volume volumePlayerOne;
    public Volume volumePlayerTwo;
    //========
    //GETTERS AND SETTERS
    //========
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
        CheckIfPlayerWin();
        if ((int)generalTimer <= 0)
        {
            textTimerGeneral.text = "0";
            //End
            if(timerPlayerOne > timerPlayerTwo)
            {
                PlayerWin(1);
            }
            else if(timerPlayerOne < timerPlayerTwo)
            {
                PlayerWin(2);
            }
        }
        else
        {
            textTimerGeneral.text = ((int)generalTimer).ToString();
        }

    }

    private void CheckIfPlayerWin()
    {
        if(TimerOne >= 60)
        {
            PlayerWin(1);
            TimerOne = 60;
        }
        else if (TimerTwo >= 60)
        {
            PlayerWin(2);
            TimerTwo = 60;
        }
    }

    public void PlayerOneGetTheCrown()
    {
        playerWhoHasTheCrown = 1;

        crownPlayerOne.SetActive(true);
        crownPlayerTwo.SetActive(false);

        DOVirtual.Float(0, 1, 1, v => volumePlayerOne.weight = v);
        DOVirtual.Float(1, 0, 1, v => volumePlayerTwo.weight = v);
    }
    public void PlayerTwoGetTheCrown()
    {
        playerWhoHasTheCrown = 2;

        crownPlayerOne.SetActive(false);
        crownPlayerTwo.SetActive(true);

        DOVirtual.Float(1, 0, 1, v => volumePlayerOne.weight = v);
        DOVirtual.Float(0, 1, 1, v => volumePlayerTwo.weight = v);
    }
    public void DropTheCrown(Vector3 position)
    {
        GameObject crown = Instantiate(crownPrefab, position, Quaternion.identity);
        playerWhoHasTheCrown = 0;
        crown.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10;
    }
    private void PlayerWin(int numberOfThePlayer)
    {
        winning_Canvas.SetActive(true);
        textWinning.text = "Player " + numberOfThePlayer + "Win";
    }
}
