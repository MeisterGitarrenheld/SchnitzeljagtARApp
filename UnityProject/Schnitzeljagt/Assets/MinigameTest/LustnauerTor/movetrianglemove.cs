using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetrianglemove : MonoBehaviour
{

    public int JumpCount = 0;
    public float speed = 50;
    public float power = 12;


    //Optional: in case if we want change speeds depending on difficulty
    //otherwise to be initialized up

    void Update()
    {
        MakeTheMove();
    }

    void MakeTheMove()
    {
        float moveOnX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveOnX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        if (Input.GetKeyDown("space") && JumpCount < 1)
        {
            Jump();
            JumpCount += 1;
        }
    }
    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, power), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Block")
        {
            JumpCount = 0;
        }
    }

}