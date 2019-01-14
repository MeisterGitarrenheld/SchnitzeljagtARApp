using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class win : MonoBehaviour {
    public Text winwin;
    public Text timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winwin.text = "Wow, you won";
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        timer.text = "WIN WIN";
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

}
