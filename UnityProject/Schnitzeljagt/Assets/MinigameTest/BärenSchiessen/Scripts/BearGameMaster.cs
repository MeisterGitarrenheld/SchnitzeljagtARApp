using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGameMaster : MonoBehaviour {

    public static BearGameMaster Instance;

    public GameObject Bear;
    public Transform[] BearSpawn;
    public bool[] LaneBlocked;

    private float timer;
	

	void Update ()
    {
		if(timer < 0)
        {
            int selectedLane = Random.Range(0, 3);
            if (LaneBlocked[selectedLane])
            {
                timer = Random.Range(0.7f, 1f);
            }
            else
            {
                Instantiate(Bear, BearSpawn[selectedLane].position, Quaternion.identity);
                timer = Random.Range(1.5f, 4f);
            }
        }

        timer -= Time.deltaTime;
	}




}
