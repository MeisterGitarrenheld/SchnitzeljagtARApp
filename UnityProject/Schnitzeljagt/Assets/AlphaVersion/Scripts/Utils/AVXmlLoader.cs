using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

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
        foreach (XmlNode node in childNodes)
        {
            if(node.Name == "characters")
                ParseCharacters(node);
            else if(node.Name == "Animations")
                ParseAnimations(node);
            else if (node.Name == "randomfacts")
                ParseRandomFacts(node);
            else if (node.Name == "chapter")
                ParseChapter(node);
        }
    }

    void ParseCharacters(XmlNode charNode)
    {
        //ToDo: CharacterHandler Store Character

        foreach (XmlNode node in charNode)
            mgm.CharacterManager.Characters.Add(
                node.Attributes["tag"].Value, 
                mgm.AssetManager.GetCharacter(node.Attributes["name"].Value));

        print(mgm.CharacterManager.Characters.Keys.Count);

    }

    void ParseAnimations(XmlNode animNode)
    {
        //ToDo: CharacterHandler Store Animations
    }

    void ParseRandomFacts(XmlNode rndNode)
    {
        //ToDo: RandomFactManager SetRandomFacts
        foreach (XmlNode fact in rndNode)
        {
            try
            {
                double lat = double.Parse(fact.ChildNodes[0].ChildNodes[0].Value);
                double lon = double.Parse(fact.ChildNodes[0].ChildNodes[1].Value);
                double alt = double.Parse(fact.ChildNodes[0].ChildNodes[2].Value);
                mgm.RandomFactManager.AddLocation(new GeoPoint(lat, lon, alt), fact.ChildNodes[1].Value);
            }
            catch (Exception e)
            {

            }
        }
    }

    void ParseChapter(XmlNode chptNode)
    {
        //ToDo: ChapterManager set Chapter

        Dictionary<int, PlainText> pTexts = new Dictionary<int, PlainText>();
        Dictionary<int, Event> events = new Dictionary<int, Event>();
        Dictionary<int, MiniGame> mGames = new Dictionary<int, MiniGame>();

        XmlNode dialog = chptNode.ChildNodes[0];

        foreach (XmlNode node in chptNode.ChildNodes)
        {
            if (node.Name == "dialog")
                dialog = node;
        }

        foreach (XmlNode dialogNode in dialog.ChildNodes)
        {
            //print(dialogNode.Name);
            if(dialogNode.Name.Equals("plainText"))
            {
                PlainText pTxt = new PlainText();
                pTxt.text = dialogNode.InnerText;
                pTxt.charID = dialogNode.Attributes["cTag"].Value;
                pTexts.Add(int.Parse(dialogNode.Attributes["num"].Value), pTxt);
                //print(int.Parse(dialogNode.Attributes["num"].Value));
            }
            else if (dialogNode.Name.Equals("minispiel"))
            {
                MiniGame mGame = new MiniGame();
                mGame.miniSpielID = dialogNode.Attributes["mId"].Value;
                mGames.Add(int.Parse(dialogNode.Attributes["num"].Value), mGame);
                //print(int.Parse(dialogNode.Attributes["num"].Value));
            }
        }
        
        Chapter cpt = new Chapter();
        cpt.pTexts = pTexts;
        cpt.events = events;
        cpt.mGame = mGames;
        mgm.ChapterManager.AddChapter(cpt);

    }
}
