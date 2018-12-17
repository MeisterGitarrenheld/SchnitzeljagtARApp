using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    public AVXmlLoader XmlLoader { get; private set; }
    public ChapterManager ChapterManager { get; private set; }
    public RandomFactManager RandomFactManager { get; private set; }
    public CharacterManager CharacterManager { get; private set; }
    public AssetManager AssetManager { get; private set; }
    public SaveLoadManager SaveLoadManager { get; private set; }
    public GlobalLocationScript GlobalLocationManager { get; private set; }

    void Awake()
    {
        Instance = this;

        XmlLoader = GetComponent<AVXmlLoader>();
        ChapterManager = GetComponent<ChapterManager>();
        RandomFactManager = GetComponent<RandomFactManager>();
        CharacterManager = GetComponent<CharacterManager>();
        AssetManager = GetComponent<AssetManager>();
        SaveLoadManager = GetComponent<SaveLoadManager>();
        GlobalLocationManager = GetComponent<GlobalLocationScript>();
    }



}
