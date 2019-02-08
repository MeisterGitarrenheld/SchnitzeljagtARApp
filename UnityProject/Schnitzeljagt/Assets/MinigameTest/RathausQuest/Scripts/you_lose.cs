using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class you_lose : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D collision)
    {
        CentralCount.health = 0;
        Application.LoadLevel(Application.loadedLevel);
    }
}
