using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truhe : MonoBehaviour {

   
    public float MovementSpeed;
    public Text PointsText;

    private float yPosition;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private int Points = 0;





    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        //Points = 0;

        PointsText.text = "Points: " + Points.ToString();
        yPosition = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {

        float horizontalMovement = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime * 60f;
        Vector2 movement = new Vector2(horizontalMovement, 0);
        rb.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sprechblase") {
            Points += 5;
            print("+5");
            Destroy(other.gameObject);
            PointsText.text = "Points: " + Points.ToString();
        }
        if(other.tag == "SprechblaseFalsch") {
            Points -= 1;
            print("-1");
            Destroy(other.gameObject);
            PointsText.text = "Points: " + Points.ToString();
        }
    }

    void SetCountText () {
        PointsText.text = "Points: " + Points.ToString();
    }
}
