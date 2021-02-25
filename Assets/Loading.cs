using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    [SerializeField] TMP_Text loadingText;
    [SerializeField] int numberOfDot = 3;
    [SerializeField] float repeatDotTime = 2f;
    int currentDot;
    void Start()

    {

        InvokeRepeating("loadingDot", 0, repeatDotTime);

    }

    void loadingDot()

    {


        if (currentDot >= numberOfDot)

        {

            currentDot = 0;

            loadingText.text = "Loading";

        }

        else

        {

            currentDot += 1;

            loadingText.text = loadingText.text + ".";

        }

    }
}