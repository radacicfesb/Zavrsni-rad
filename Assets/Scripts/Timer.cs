using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;

    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] TMP_Text scoreText;
    string timer;
    //float t;

    PlayFabLogin1 playfab;
    void Start()
    {
        startTime = Time.time;
        playfab = FindObjectOfType<PlayFabLogin1>();
    }

    void Update()
    {
        if (playerMovement.alive)
        {
           float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");

           
            timerText.text = minutes + ":" + seconds;

            if ((int)playerMovement.moveSpeed == 30)
                playerMovement.moveSpeed = 30;
            else
            {
                if (((int)t / 600) % 13 == 0)//random brojevi da sta vise usporin ubrzavanje
                    playerMovement.moveSpeed += playerMovement.speedIncreasePerSeconds;
            }
         } 
        timer = timerText.text;
        playfab.playerHighScore = (int)(Time.time - startTime);
    }

    public void AddFiveSecondsToTimer()
    {
        startTime -= 5;
    }

    public void TakeFiveSecondsFromTimer()
    {
        startTime += 5;
    }

    public void PrintScore()
    {
        scoreText.text = timer;
    }
}
