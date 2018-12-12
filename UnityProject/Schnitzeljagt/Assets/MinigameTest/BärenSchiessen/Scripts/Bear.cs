using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {


    public float MoveSpeed;

    
	void Start () {
        Destroy(gameObject, 30);
	}
	
	void Update () {
        transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meat"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Honey"))
        {
            Destroy(gameObject, 5f);
            MoveSpeed = 0;
            Destroy(other.gameObject);
        }
    }
}
