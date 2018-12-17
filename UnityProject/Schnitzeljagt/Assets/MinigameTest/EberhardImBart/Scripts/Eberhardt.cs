using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eberhardt : MonoBehaviour {

    public float MaxMoveRange;

    private float speed = 5;
    private Rigidbody rb;
    private int ebene = 1;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < -MaxMoveRange)
            speed = 5 * ebene;
        else if (transform.position.x > MaxMoveRange)
            speed = -5 * ebene;

        transform.position += Vector3.right * speed * Time.deltaTime;

	}



    public void GoUpEbene()
    {
        ebene++;
        transform.position += Vector3.up * 0.6f;
        transform.localScale *= 0.8f;
    }

}
