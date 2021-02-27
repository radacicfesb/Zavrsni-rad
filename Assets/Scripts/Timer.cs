using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    private float startTime;

    [SerializeField] PlayerMovement playerMovement;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        //if (playerMovement.timerForTimer)
       // {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;

            if ((int)playerMovement.moveSpeed == 30)
                playerMovement.moveSpeed = 30;
            else
            {
                if (((int)t / 600) % 13 == 0)//random brojevi da sta vise usporin ubrzavanje
                    playerMovement.moveSpeed += playerMovement.speedIncreasePerSeconds;
            }
        //}
    }

    public void AddFiveSecondsToTimer()
    {
        startTime -= 5;
    }

    public void TakeFiveSecondsFromTimer()
    {
        startTime += 5;
    }
}
