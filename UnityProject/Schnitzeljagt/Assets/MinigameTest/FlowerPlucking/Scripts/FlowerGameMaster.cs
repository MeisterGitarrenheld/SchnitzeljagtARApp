using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Game Starts when in bota
/// Game is started on the field
/// start location is saved
/// when moved chance increases to find flower
/// flower point is saved and when moved some meters flowers can increase again
/// </summary>
public class FlowerGameMaster : MonoBehaviour {

    //Debugging:
    public Text DebugUIText;

    public List<Sprite> FlowerSprites;
    public GameObject FlowerInstance;
    public FlowerCamera FlowerCamera;

    public float NextFlowerDistance = 5;
    public float ChanceIncreasePerDistance = 10;
    public float DistanceDivisor = 5000;

    // Phone Attributes
    // Vibration
    public float VibrationIntervall;
    public float VibrationTime;

    private float vibrationTimer;

    private GeoPoint lastLocation;
    private GlobalLocationScript locationScript;

    private bool flowerSpawned;
    private double currentSpawnChance;

    private void Start()
    {
        locationScript = GlobalGameManager.Instance.GetComponent<GlobalLocationScript>();
        lastLocation = locationScript.GetCurrentLocation();
    }


    private void Update()
    {
        if (flowerSpawned)
        {
            vibrationTimer += Time.deltaTime;
            if (vibrationTimer > VibrationIntervall && vibrationTimer < VibrationIntervall + VibrationTime)
                Handheld.Vibrate();
            else if (vibrationTimer >= VibrationIntervall + VibrationTime)
                vibrationTimer = 0;
        }
        else
        {
            if (Random.Range(0.0f, 100.0f) < currentSpawnChance)
            {
                flowerSpawned = true;
                Instantiate(FlowerInstance);
            }
            if (lastLocation.Distance(locationScript.GetCurrentLocation()) / DistanceDivisor < NextFlowerDistance)
                currentSpawnChance = -1.0;
            else
                currentSpawnChance = lastLocation.Distance(locationScript.GetCurrentLocation()) / DistanceDivisor;

            currentSpawnChance += Time.deltaTime;
        }
        DebugUIText.text =
            "Location:" + locationScript.GetCurrentLocation() +" \n" +
            "Last Location: " + lastLocation +" \n" +
            "Distance:" + lastLocation.Distance(locationScript.GetCurrentLocation()) +" \n" +
            "Chance: " + currentSpawnChance;
    }


    public void PluckFlower(Flower flowerPlucked)
    {
        lastLocation = locationScript.GetCurrentLocation();
        currentSpawnChance = -1.0f;
    }
}
