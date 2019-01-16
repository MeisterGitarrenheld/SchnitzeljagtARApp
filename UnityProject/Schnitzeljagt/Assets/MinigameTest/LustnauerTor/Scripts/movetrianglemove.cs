using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetrianglemove : MonoBehaviour
{

    public int JumpCount = 0;
    public float speed = 50;
    public float power = 12;
    public bool RightMaybe;

    //Optional: in case if we want change speeds depending on difficulty
    //otherwise to be initialized up

    void Update()
    {
        MakeTheMove();
    }

    void MakeTheMove()
    {
        RightMaybe = true;
        float moveOnX = Input.GetAxis("Mouse X");
        Vector3 movement = new Vector3(moveOnX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
        movement *= Time.deltaTime;
        //FlipFlip(moveOnX);
        transform.Translate(movement);
        
        if (moveOnX < 0)
        {
            GetComponent<Animator>().SetBool("RightMaybe", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("RightMaybe", false);
        }

        if (Input.GetMouseButtonDown(0) && JumpCount < 1)
        {
            Jump();
            JumpCount += 1;
        }


    }
    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, power), ForceMode2D.Impulse);
    }

    void FlipFlip(float moveOnX)
    {
        if (moveOnX > 0 && !RightMaybe || moveOnX < 0 && RightMaybe)
            {
            RightMaybe = !RightMaybe;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Block")
        {
            JumpCount = 0;
        }
    }

}