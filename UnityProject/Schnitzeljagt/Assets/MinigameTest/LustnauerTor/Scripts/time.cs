using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class time : MonoBehaviour
{
    public int timeLeft = 90; 
    public Text countdown; 

    void Start()
    {
        StartCoroutine("LoseTime");
    }
    void Update()
    {
        countdown.text = ("Time Left: " + timeLeft); 
        if(timeLeft == 0 && timeLeft<0)
        {
            countdown.text = ("That's really embarassing");

        }
    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}