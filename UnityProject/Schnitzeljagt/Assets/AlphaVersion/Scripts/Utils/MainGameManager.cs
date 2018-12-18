using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public VuforiaMonoBehaviour ArCamera;

    public Transform PlainTextContainer;

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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChapterManager.Progress();
    }

    public void ToggleCamera()
    {
        ArCamera.enabled = !ArCamera.enabled; 
    }


    public void DisplayPlainText(string lText, string rText, GameObject lChar, GameObject rChar)
    {
        //PlainTextContainer.GetChild(0).GetComponent<Image>().sprite = lChar.GetComponent<Image>().sprite;
        //PlainTextContainer.GetChild(0).GetComponent<BouncePulse>().StartAnimating();
        PlainTextContainer.GetChild(1).GetComponent<Image>().sprite = rChar.GetComponent<Image>().sprite;
        PlainTextContainer.GetChild(1).GetComponent<BouncePulse>().StartAnimating();
        //PlainTextContainer.GetChild(2).GetComponentInChildren<Text>().text = lText;
        PlainTextContainer.GetChild(3).GetComponentInChildren<Text>().text = rText;

    }

    public void StartEvent(string eID)
    {

    }

    public void DisplayRandomFact(string fact)
    {

    }

    public void StartMiniGame(string mID)
    {
        switch(mID)
        {
            case "Wortschatz":
                SceneManager.LoadScene(2);
                break;
            default: break;
        }
    }

}
