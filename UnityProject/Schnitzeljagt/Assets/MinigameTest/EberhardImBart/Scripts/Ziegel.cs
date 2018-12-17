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

    public float ZPosition {get; private set;}
    public float ZSpeed;

    // Use this for initialization
    void Start () {
        PointsText = GameObject.Find("Text").GetComponent<Text>();
        PointsText.text = "Points: " + Points.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        ZPosition += ZSpeed * Time.deltaTime;
	}


    void OnTriggerEnter(Collider other)
    {
        print("Collision");
        if (other.tag == "Character")
        {
            Points += PointsToGet;
            PointsToGet += 5;
            print("+" + PointsToGet);
            Destroy(gameObject);
            PointsText.text = "Points: " + Points.ToString();
            other.GetComponent<Eberhardt>().GoUpEbene();
            print(ZPosition);
        }
        else if (other.tag == "Stair")
        {
            Points -= 5;
            print("-5");
            Destroy(gameObject);
            PointsText.text = "Points: " + Points.ToString();
        }
    }

}
