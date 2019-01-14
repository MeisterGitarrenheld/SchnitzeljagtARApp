using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGameMaster : MonoBehaviour {

    public List<Sprite> BlumenSprites;
    public List<GeoPoint> flowerPoints;
    public List<GeoPoint> rightFlowerPoints;
    public List<GeoPoint> allPoints;
    public float FlowerCheckInterval;
    public GameObject Instructions;
    public FlowerCamera fCam;
    public GameObject Blume;

    GlobalLocationScript gLoc;
    XmlDocument xmlDocument;
    TextAsset xmlAsset;
    float flowerCheckTimer;
    bool startGame;
    float resetCamTimer;
    bool triggerBlume;
    List<Sprite> blSprite;
    GameObject blumenInstanz;




    GeoPoint testLocation;

    public List<GeoPoint> collectedPoints;

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
        for (int i = 0; i < 20; i++)
        {
            int rndSelect = Random.Range(0, flowerPoints.Count);
            flowerPoints.RemoveAt(rndSelect);
        }
        allPoints = new List<GeoPoint>();
        allPoints.AddRange(rightFlowerPoints);
        allPoints.AddRange(flowerPoints);

        startGame = true;


        testLocation = gLoc.GetCurrentLocation();
        allPoints.Add(testLocation);

        collectedPoints = new List<GeoPoint>();
        blSprite = BlumenSprites;
    }
	
	void Update ()
    {
        if (startGame)
        {
            if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                fCam.ResetGyroCamera();
                startGame = false;
            }
        }
        else
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                resetCamTimer += Time.deltaTime;
                if(resetCamTimer > 2)
                {
                    resetCamTimer = 0;
                    fCam.ResetGyroCamera();
                }
            }
            


            if (flowerCheckTimer < 0)
            {
                flowerCheckTimer = FlowerCheckInterval;

                foreach (GeoPoint flowerGP in allPoints)
                {
                    
                    if (flowerGP.Compare(gLoc.GetCurrentLocation(), 1/10000f) && !collectedPoints.Contains(flowerGP))
                    {
                        if(blumenInstanz != null)
                            Handheld.Vibrate();
                        if(blumenInstanz == null)
                        {
                            blumenInstanz = Instantiate(Blume, transform.position + Vector3.forward , Quaternion.identity);
                            blumenInstanz.GetComponentInChildren<Blume>().ownPoint = flowerGP;
                            int randSprt = Random.Range(0, blSprite.Count);
                            blumenInstanz.GetComponentInChildren<SpriteRenderer>().sprite = blSprite[randSprt];
                            blSprite.RemoveAt(randSprt);
                            if (blSprite.Count == 0)
                                blSprite = BlumenSprites;
                        }
                    }

                }
            }
            flowerCheckTimer -= Time.deltaTime;
        }
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
