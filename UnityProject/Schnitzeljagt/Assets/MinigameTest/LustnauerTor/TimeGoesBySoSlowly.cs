using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeGoesBySoSlowly : MonoBehaviour
{
    float timeLeft = 90.0f;
    public Text text;
    public Text gameOver;


    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text =  "Time:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            gameOver.text = "Time's up!";
        }
    }
}