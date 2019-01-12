using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyStar1 : MonoBehaviour {

    public GameObject star1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        healthyhealth.stars -= 1;
        star1.gameObject.SetActive(false);
    }
}
