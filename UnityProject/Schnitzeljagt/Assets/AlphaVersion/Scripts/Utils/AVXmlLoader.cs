using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class AVXmlLoader : MonoBehaviour
{
    MainGameManager mgm;

    XmlDocument xmlDocument;
    string path;

    void Start()
    {
        mgm = MainGameManager.Instance;
        path = Application.dataPath + "/AlphaVersion/Xml Files/" + "Altstadt_Chapter_1_von_der_Tourist_Info_zum_Holderlinturm" + ".xml";
        LoadXML();
    }

    void LoadXML()
    {
        if (!File.Exists(path))
        {
            print("Path: " + path + " does not exist.");
            return;
        }
        xmlDocument = new XmlDocument();
        xmlDocument.Load(path);

        XmlNodeList childNodes = xmlDocument.ChildNodes[0].ChildNodes;
        ParseCharacters(childNodes[0]);
        ParseAnimations(childNodes[1]);
        ParseRandomFacts(childNodes[2]);
        ParseChapter(childNodes[3]);
    }

    void ParseCharacters(XmlNode charNode)
    {
        //ToDo: CharacterHandler Store Character

        foreach (XmlNode node in charNode)
            mgm.CharacterManager.Characters.Add(
                node.Attributes["tag"].Value, 
                mgm.AssetManager.GetCharacter(node.Attributes["name"].Value));
        
    }

    void ParseAnimations(XmlNode animNode)
    {
        //ToDo: CharacterHandler Store Animations
    }

    void ParseRandomFacts(XmlNode rndNode)
    {
        //ToDo: RandomFactManager SetRandomFacts
        foreach(XmlNode fact in rndNode)
        {
            double lat = double.Parse(fact.ChildNodes[0].ChildNodes[0].Value);
            double lon = double.Parse(fact.ChildNodes[0].ChildNodes[1].Value);
            double alt = double.Parse(fact.ChildNodes[0].ChildNodes[2].Value);
            mgm.RandomFactManager.AddLocation(new GeoPoint(lat, lon, alt), fact.ChildNodes[1].Value);
        }
    }

    void ParseChapter(XmlNode chptNode)
    {
        //ToDo: ChapterManager set Chapter
    }
}
