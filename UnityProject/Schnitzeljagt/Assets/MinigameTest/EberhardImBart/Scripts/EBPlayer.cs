using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBPlayer : MonoBehaviour {

    public GameObject Ziegel;

    private Transform currentZiegel;
    private float zGeschw = 500;


	// Use this for initialization
	void Start () {
 

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            currentZiegel = Instantiate(Ziegel, 
                                        Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10, 
                                        Quaternion.identity).transform;
        }
        if(currentZiegel != null && Input.GetMouseButton(0))
        {
            currentZiegel.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        }
        if(currentZiegel != null && Input.GetMouseButtonUp(0))
        {
            currentZiegel.GetComponent<Rigidbody>().velocity = Vector3.up * Input.GetAxis("Mouse Y") * 5 + Vector3.forward * zGeschw;
            currentZiegel = null;
        }
	}
}
