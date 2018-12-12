using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    GlobalGameManager gm;


    void Start()
    {
        gm = GlobalGameManager.Instance;
        GameObject.Find("StartGame").GetComponent<Button>().onClick.AddListener(delegate { gm.LoadScene("Game"); });
    }

    
}
