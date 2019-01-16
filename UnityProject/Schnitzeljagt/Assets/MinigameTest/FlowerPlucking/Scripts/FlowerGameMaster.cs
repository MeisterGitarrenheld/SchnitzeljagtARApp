using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGameMaster : MonoBehaviour {

    public List<Sprite> BlumenSprites;
    public List<GeoPoint> flowerPoints;
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

    bool flowerNear;
    GeoPoint currentFP;

    public List<GeoPoint> collectedPoints;

    public int Points;
    public float GameTimer;


    private int flowersPlucked;
	void Start ()
    {
        gLoc = GameObject.Find("GlobalGameManager").GetComponent<GlobalLocationScript>();

        string xmlDocumentName = "xml Dateien Neckarquest/Botanischer_Garten_Geopunkte_Pflanzen";
        xmlAsset = (TextAsset) Resources.Load(xmlDocumentName, typeof(TextAsset));

        flowerPoints = new List<GeoPoint>();
        LoadFlowerPositions();
        
        startGame = true;


        testLocation = gLoc.GetCurrentLocation();
        flowerPoints.Add(testLocation);
        collectedPoints = new List<GeoPoint>();
        blSprite = BlumenSprites;

        Points = 0;
        GameTimer = 0;
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

                flowerNear = false;
                currentFP = null;
                foreach (GeoPoint flowerGP in flowerPoints)
                {
                    
                    if (flowerGP.Compare(gLoc.GetCurrentLocation(), 5/100000f) && !collectedPoints.Contains(flowerGP))
                    {

                        flowerNear = true;
                        currentFP = flowerGP;
                    }

                }
            }
            flowerCheckTimer -= Time.deltaTime;
            if(flowerNear)
            {

                Handheld.Vibrate();
                if (blumenInstanz == null)
                {
                    blumenInstanz = Instantiate(Blume, transform.position + Vector3.forward, Quaternion.identity);
                    blumenInstanz.GetComponentInChildren<Blume>().ownPoint = currentFP;
                    int randSprt = Random.Range(0, blSprite.Count);
                    blumenInstanz.GetComponentInChildren<SpriteRenderer>().sprite = blSprite[randSprt];
                    blSprite.RemoveAt(randSprt);
                    if (blSprite.Count == 0)
                        blSprite = BlumenSprites;
                }
            }
            else
            {
                if(blumenInstanz != null)
                {
                    Destroy(blumenInstanz);
                }
            }
        }
	}

    public void PluckFlower(Blume flower)
    {
        collectedPoints.Add(flower.ownPoint);
        flowersPlucked++;
        Points += flower.Poisonous ? 0 :  flower.Fuchsia ? 30 : 10;
        GameTimer += flower.Poisonous ? 10 : flower.Fuchsia ? -30 : 0;
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
