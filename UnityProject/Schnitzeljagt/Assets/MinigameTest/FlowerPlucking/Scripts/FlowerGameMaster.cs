using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGameMaster : MonoBehaviour {

    public List<GeoPoint> flowerPoints;
    public List<GeoPoint> rightFlowerPoints;
    public float FlowerCheckInterval;

    GlobalLocationScript gLoc;

    XmlDocument xmlDocument;
    TextAsset xmlAsset;

    float flowerCheckTimer;

	void Start ()
    {
        gLoc = GameObject.Find("GlobalGameManager").GetComponent<GlobalLocationScript>();

        string xmlDocumentName = "xml Dateien Neckarquest/Botanischer_Garten_Geopunkte_Pflanzen";
        xmlAsset = (TextAsset) Resources.Load(xmlDocumentName, typeof(TextAsset));

        flowerPoints = new List<GeoPoint>();
        rightFlowerPoints = new List<GeoPoint>();
        LoadFlowerPositions();

        for(int i = 0; i < 10; i++)
        {
            int rndSelect = Random.Range(0, flowerPoints.Count);
            GeoPoint gp = flowerPoints[rndSelect];
            flowerPoints.RemoveAt(rndSelect);
            rightFlowerPoints.Add(gp);
        }
	}
	
	void Update ()
    {
        if(flowerCheckTimer < 0)
        {
            flowerCheckTimer = FlowerCheckInterval;

            foreach(GeoPoint flowerGP in rightFlowerPoints)
            {
                if (flowerGP.Compare(gLoc.GetCurrentLocation(), 5))
                {
                    Handheld.Vibrate();
                }
            }

        }
        flowerCheckTimer -= Time.deltaTime;
	}

    void LoadFlowerPositions()
    {
        xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlAsset.text);

        XmlNodeList childNodes = xmlDocument.GetElementsByTagName("geopunkt");
        
        foreach (XmlNode node in childNodes)
        {
            if (node.Name == "geopunkt")
            {
                double lat = 0;
                double lon = 0;
                double alt = 0;
                foreach(XmlNode geoNode in node.ChildNodes)
                {
                    if (geoNode.Name == "Latitude")
                    {
                        double.TryParse(geoNode.InnerText, out lat);
                    }
                    else if (geoNode.Name == "Longitude")
                    {
                        double.TryParse(geoNode.InnerText, out lon);
                    }
                    else if (geoNode.Name == "Altitude")
                    {
                        double.TryParse(geoNode.InnerText, out alt);
                    }
                }
                GeoPoint gp = new GeoPoint(lat, lon, alt);
                flowerPoints.Add(gp);
            }
        }
    }
}
