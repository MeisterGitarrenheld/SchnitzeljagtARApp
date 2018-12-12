using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour {


    public float MoveSpeed;

    
	void Start () {
        Destroy(gameObject, 30);
	}
	
	void Update () {
        transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
