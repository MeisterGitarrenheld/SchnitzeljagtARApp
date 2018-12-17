using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eberhardt : MonoBehaviour {

    public float MaxMoveRange;
    public float MoveSpeed = 1;
    public float EbenenMultiplikator = 1.2f;


    private float speed;
    private Rigidbody rb;
    private int ebene = 1;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        speed = MoveSpeed * EbenenMultiplikator * ebene;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < -MaxMoveRange)
            speed = MoveSpeed * EbenenMultiplikator * ebene;
        else if (transform.position.x > MaxMoveRange)
            speed = -MoveSpeed * EbenenMultiplikator * ebene;

        transform.position += Vector3.right * speed * Time.deltaTime;

    }



    public void GoUpEbene()
    {
        ebene++;
        transform.position += Vector3.up * 0.6f;
        transform.position += Vector3.forward;
    }

}
