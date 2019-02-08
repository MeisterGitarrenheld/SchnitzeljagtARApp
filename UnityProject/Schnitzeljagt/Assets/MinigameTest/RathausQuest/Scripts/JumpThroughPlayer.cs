using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughPlayer : MonoBehaviour {

    public float JumpForce;
    public float MoveSpeed;

    private Rigidbody rb;
    private BoxCollider col;

    public bool facingLeft, isWalking;


	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
	}
	
	void Update ()
    {
        GetComponent<Animator>().SetBool("isWalking", true);
        float xMove = Input.GetAxis("Horizontal");

        if (xMove < 0)
        {
            GetComponent<Animator>().SetBool("facingLeft", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("facingLeft", false);
        }

        transform.position += Vector3.right * xMove * MoveSpeed * Time.deltaTime;
        
        if(RaycastGround() && rb.velocity.y <= 0)
        {
            
            rb.velocity = new Vector3(rb.velocity.x, JumpForce);
            GetComponent<Animator>().SetBool("isWalking", false);
        }
	}


    bool RaycastGround()
    {
        return Physics.Raycast(transform.position + Vector3.down * col.bounds.size.y / 2f, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("DoodleFloor"));
    }
}
