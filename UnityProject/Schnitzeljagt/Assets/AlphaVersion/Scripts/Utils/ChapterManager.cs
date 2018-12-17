using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Chapter
{
    public GeoPoint location;
    public Dictionary<int, PlainText> pTexts;
    public Dictionary<int, Event> events;
    public Dictionary<int, MiniGame> mGame;

}

public struct PlainText
{
    public string text;
}

public struct Event
{
    public string text;
}

public struct MiniGame
{
    public int miniSpielID;
}

public class ChapterManager : MonoBehaviour
{
    

    public List<Chapter> Chapters { get; private set; }

    void Start()
    {
        Chapters = new List<Chapter>();
    }
    
    public void AddChapter(Chapter chapter)
    {
        Chapters.Add(chapter);
    }

}
