using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Frage
{

    public string Question;
    public string[] RightAnswer;
    public string[] FalseAnswer;

    // Number of right answers, so position can be random
    public int RightNumberOfAnswers;
    public List<int> RightNums;

    public Frage(string q, string[] rA, string[] fA, int rN)
    {
        Question = q;
        RightAnswer = rA;
        FalseAnswer = fA;
        RightNumberOfAnswers = rN;
        RightNums = new List<int>();
        for(int i = 0; i < RightNumberOfAnswers; i++)
        {
            int rand = Random.Range(0, 4);
            while (RightNums.Contains((rand = Random.Range(0, 4)))) ;
            RightNums.Add(rand);
        }
    }
}

public class FrageMaster : MonoBehaviour
{
    public static FrageMaster Instance;

    public Transform FrageUICanvas;
    public float MaxTime;

    private float timer;
    private Frage currentFrage;
    private bool gameOver;

    private bool fiftyFifty;
    private bool trivia;
    private bool internet;
    List<int> wrongAnswers;

    void Start()
    {
        Instance = this;
        timer = MaxTime;

        NewQuestion();
    }

    void Update()
    {
        if(timer < 0)
        {
            print("GameOver");
            timer = MaxTime;
            NewQuestion();
        }

        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = MaxTime;
            NewQuestion();
        }

    }


    void NewQuestion()
    {
        wrongAnswers = new List<int>();
        for (int i = 1; i < 5; i++)
            FrageUICanvas.GetChild(i).gameObject.SetActive(true);
        int questionSelect = Random.Range(0, 2);
        switch (questionSelect)
        {
            default:
            case 0:
                currentFrage = new Frage(
                "Worin war der heutige Hölderlinturm im Ursprung integriert?",
                new string[] { "In die Stadtmauer" }, new string[] { "In das alte Klinikum", "In die neue Aula", "In die Irrenanstalt" },
                1);
                break;
            case 1:
                currentFrage = new Frage(
                "Ab welchem Jahrhundert wurde aus dem heutigen Hölderlinturm ein Wohngebäude?",
                new string[] { "18. Jahrhundert"}, new string[] { "17. Jahrhundert", "16. Jahrhundert", "19. Jahrhundert" },
                1);
                break;
        }

        FrageUICanvas.GetChild(0).GetComponentInChildren<Text>().text = currentFrage.Question;

        List<int> freeNums = new List<int>() { 1, 2, 3, 4 };

        for (int i = 0; i < currentFrage.RightNumberOfAnswers; i++)
        {
            int rightIndex = currentFrage.RightNums[i] + 1;
            FrageUICanvas.GetChild(rightIndex).GetComponentInChildren<Text>().text = currentFrage.RightAnswer[i];
            FrageUICanvas.GetChild(rightIndex).GetComponent<Answer>().correct = true;
            freeNums.Remove(rightIndex);
        }

        for (int i = 0; i < 4 - currentFrage.RightNumberOfAnswers; i++)
        {
            FrageUICanvas.GetChild(freeNums[i]).GetComponentInChildren<Text>().text = currentFrage.FalseAnswer[i];
            FrageUICanvas.GetChild(freeNums[i]).GetComponent<Answer>().correct = false;
            wrongAnswers.Add(freeNums[i]);
        }
    }

    public void Answered(bool correct)
    {
        if (correct)
            print("Right Answer.");
        else
            print("Wrong Answer");

        timer = MaxTime;
        NewQuestion();
    }

    public void FiftyFifty()
    {
        if (fiftyFifty)
            return;
        for(int i = 0; i < 2; i++)
        {
            int rndWrongAnswer = wrongAnswers[Random.Range(0, wrongAnswers.Count)];
            FrageUICanvas.GetChild(rndWrongAnswer).gameObject.SetActive(false);
            wrongAnswers.Remove(rndWrongAnswer);
        }
        fiftyFifty = true;
    }

    public void Internet()
    {
        if (internet)
            return;
        PlayerPrefs.SetString("Internet", "Active");
    }

    public void Trivia()
    {
        if (trivia)
            return;
    }
}
