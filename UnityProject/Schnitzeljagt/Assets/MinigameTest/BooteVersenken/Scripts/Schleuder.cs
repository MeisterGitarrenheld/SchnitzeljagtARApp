using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schleuder : MonoBehaviour {

    public GameObject Rock;
    public float mouseDistanceFactor;
    public float maxForce;

    Vector3 MousePosition;
    Vector3 OldMousePosition;

    private Vector3 ForceVector;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OldMousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            MousePosition = Input.mousePosition;

            ForceVector = Vector3.ClampMagnitude((MousePosition - OldMousePosition) / mouseDistanceFactor, maxForce);

            Debug.DrawLine(transform.position, transform.position - (ForceVector / 100f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            Rigidbody rockRb = Instantiate(Rock, transform.position, transform.rotation).GetComponent<Rigidbody>();
            ForceVector = -ForceVector;
            ForceVector.z = ForceVector.y;
            //print(ForceVector);
            rockRb.AddForce(ForceVector);
        }
    }
}
