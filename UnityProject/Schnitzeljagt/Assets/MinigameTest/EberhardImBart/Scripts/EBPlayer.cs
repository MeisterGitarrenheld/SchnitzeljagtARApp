using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBPlayer : MonoBehaviour {

    public GameObject Ziegel;
    public float velocityMultiplier = 7;
    public float zGeschw = 5;

    private Transform currentZiegel;


    private Vector3 SpawnPosition;
	// Use this for initialization
	void Start () {
        SpawnPosition = Camera.main.transform.position + Vector3.down * 5 + Vector3.forward * 10;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            currentZiegel = Instantiate(Ziegel, 
                                        SpawnPosition, 
                                        Quaternion.identity).transform;
        }
        if(currentZiegel != null && Input.GetMouseButton(0))
        {
            currentZiegel.position = SpawnPosition;
        }
        if(currentZiegel != null && Input.GetMouseButtonUp(0))
        {
            currentZiegel.GetComponent<Rigidbody>().velocity = Vector3.up * Input.GetAxis("Mouse Y") * velocityMultiplier + Vector3.right * Input.GetAxis("Mouse X") * velocityMultiplier + Vector3.forward * zGeschw;
            currentZiegel = null;
        }
	}
}
