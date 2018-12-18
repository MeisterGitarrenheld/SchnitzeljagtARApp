using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFactManager : MonoBehaviour
{

    public Dictionary<GeoPoint, string> FactLocations { get; private set; }

    private MainGameManager mgm;

    void Awake()
    {
        FactLocations = new Dictionary<GeoPoint, string>();
        mgm = MainGameManager.Instance;
    }

    void Update()
    {
        foreach(GeoPoint gp in FactLocations.Keys)
        {
            if (gp.Compare(mgm.GlobalLocationManager.GetCurrentLocation(), 10))
                print(FactLocations[gp]);
        }
    }

    public void AddLocation(GeoPoint location, string text)
    {
        FactLocations.Add(location, text);
    }

}
