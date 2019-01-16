using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerGameMaster : MonoBehaviour {

    public List<Sprite> BlumenSprites;
    public List<Sprite> BlumenSpritesBad;
    public Sprite Fuchsia;
    public List<GeoPoint> flowerPoints;
    public List<GeoPoint> badFlowerPoints;
    GeoPoint fuchsiaLocation;
    public float FlowerCheckInterval;
    public GameObject Instructions;
    public FlowerCamera fCam;
    public GameObject Blume;
    public GameObject PointsUI;
    public Slider TimerSlider;

    GlobalLocationScript gLoc;
    XmlDocument xmlDocument;
    TextAsset xmlAsset;
    float flowerCheckTimer;
    bool startGame;
    float resetCamTimer;
    bool triggerBlume;
    List<Sprite> blSprite;
    List<Sprite> badBlSprite;
    GameObject blumenInstanz;

    bool flowerNear;
    bool badFlower;
    bool fuchsia;
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
        badFlowerPoints = new List<GeoPoint>();
        LoadFlowerPositions();
        
        startGame = true;

        for(int i = 0; i < 10; i++)
        {
            int rndPoint = Random.Range(0, flowerPoints.Count);
            GeoPoint flp = flowerPoints[rndPoint];
            flowerPoints.RemoveAt(rndPoint);
            badFlowerPoints.Add(flp);
        }

        int rndFuchs = Random.Range(0, flowerPoints.Count);
        fuchsiaLocation = flowerPoints[rndFuchs];
        flowerPoints.RemoveAt(rndFuchs);

        collectedPoints = new List<GeoPoint>();
        blSprite = BlumenSprites;
        badBlSprite = BlumenSpritesBad;
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
            GameTimer += Time.deltaTime;
            if (GameTimer < 600)
            {
                TimerSlider.value = GameTimer / (600f);
            }
            else
                TimerSlider.value = 1;

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
                badFlower = false;
                fuchsia = false;
                currentFP = null;
                foreach (GeoPoint flowerGP in flowerPoints)
                {
                    
                    if (flowerGP.Compare(gLoc.GetCurrentLocation(), 5/100000f) && !collectedPoints.Contains(flowerGP))
                    {

                        flowerNear = true;
                        currentFP = flowerGP;
                    }
                }
                foreach (GeoPoint flowerGP in badFlowerPoints)
                {

                    if (flowerGP.Compare(gLoc.GetCurrentLocation(), 5 / 100000f) && !collectedPoints.Contains(flowerGP))
                    {

                        flowerNear = true;
                        badFlower = true;
                        currentFP = flowerGP;
                    }
                }
                if (fuchsiaLocation.Compare(gLoc.GetCurrentLocation(), 5 / 100000f) && !collectedPoints.Contains(fuchsiaLocation))
                {

                    flowerNear = true;
                    fuchsia = true;
                    currentFP = fuchsiaLocation;
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
                    blumenInstanz.GetComponentInChildren<Blume>().Poisonous = badFlower;
                    blumenInstanz.GetComponentInChildren<Blume>().Fuchsia = fuchsia;
                    if (fuchsia)
                    {
                        blumenInstanz.GetComponentInChildren<SpriteRenderer>().sprite = Fuchsia;
                    }
                    else if (badFlower)
                    {
                        int randSprt = Random.Range(0, blSprite.Count);
                        blumenInstanz.GetComponentInChildren<SpriteRenderer>().sprite = blSprite[randSprt];
                        blSprite.RemoveAt(randSprt);
                        if (blSprite.Count == 0)
                            blSprite = BlumenSprites;
                    }
                    else
                    {
                        int randSprt = Random.Range(0, badBlSprite.Count);
                        blumenInstanz.GetComponentInChildren<SpriteRenderer>().sprite = badBlSprite[randSprt];
                        badBlSprite.RemoveAt(randSprt);
                        if (badBlSprite.Count == 0)
                            badBlSprite = BlumenSpritesBad;
                    }
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
        int points = flower.Poisonous ? 0 : flower.Fuchsia ? 30 : 10;
        int time = flower.Poisonous ? 10 : flower.Fuchsia ? -30 : 0;
        Text pUi = Instantiate(PointsUI, flower.transform.position, Quaternion.identity).GetComponentInChildren<Text>();
        pUi.text = "Points + " + points + "\n" + "Time + " + time;
        Destroy(pUi.gameObject, 2);
        Points += points;
        GameTimer += time;
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
