using System;
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
    public string charID;
}

public struct Event
{
    public string text;
}

public struct MiniGame
{
    public string miniSpielID;
}

public class ChapterManager : MonoBehaviour
{
    

    public List<Chapter> Chapters { get; private set; }
    [HideInInspector]
    public int SelectedChapter;

    private int ChapterProgress;
    private MainGameManager mgm;

    void Awake()
    {
        mgm = MainGameManager.Instance;
        Chapters = new List<Chapter>();
        if (GlobalGameManager.Instance != null)
            SelectedChapter = GlobalGameManager.Instance.SelectedChapter;
        else
            SelectedChapter = 0;
    }
    
    public void AddChapter(Chapter chapter)
    {
        Chapters.Add(chapter);
        //print(Chapters.Count);
    }

    public void Progress()
    {
        //print(ChapterProgress);

        if (Chapters[SelectedChapter].pTexts.ContainsKey(ChapterProgress))
        {
            PlainText pText = Chapters[SelectedChapter].pTexts[ChapterProgress];
            print("Text: " + pText.text);
            mgm.DisplayPlainText(pText.text, pText.text, mgm.CharacterManager.Characters[pText.charID], mgm.CharacterManager.Characters[pText.charID]);
        }
        else if (Chapters[SelectedChapter].events.ContainsKey(ChapterProgress))
        {

        }
        else if (Chapters[SelectedChapter].mGame.ContainsKey(ChapterProgress))
        {
            mgm.StartMiniGame(Chapters[SelectedChapter].mGame[ChapterProgress].miniSpielID);
        }

        ChapterProgress++;
    }

}
