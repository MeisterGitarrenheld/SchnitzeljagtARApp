using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ziegel : MonoBehaviour
{

    private Text PointsText;
    private int Points = 0;
    private int PointsToGet = 5;
    private float ebene;
    

    // Use this for initialization
    void Start () {
        PointsText = GameObject.Find("Text").GetComponent<Text>();
        PointsText.text = "Points: " + Points.ToString();
        Destroy(gameObject, 5);
	}


    
    void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        if (collision.collider.tag == "Character")
        {
            Points += PointsToGet;
            PointsToGet += 5;
            print("+" + PointsToGet);
            Destroy(gameObject);
            PointsText.text = "Points: " + Points.ToString();
            collision.collider.GetComponent<Eberhardt>().GoUpEbene();
        }
        else if (collision.collider.tag == "Stair")
        {
            Points -= 5;
            print("-5");
            Destroy(gameObject);
            PointsText.text = "Points: " + Points.ToString();
        }
    }

}
