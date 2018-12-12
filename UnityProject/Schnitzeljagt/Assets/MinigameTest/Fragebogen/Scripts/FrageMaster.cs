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
        int questionSelect = Random.Range(0, 5);
        switch (questionSelect)
        {
            default:
            case 0:
                currentFrage = new Frage(
                "Wer hat an der Uhr gedreht?",
                new string[] { "Jerry" }, new string[] { "Mickey Mouse", "Lucky Luke", "Bullet Bill" },
                1);
                break;
            case 1:
                currentFrage = new Frage(
                "Ist es wirklich schon so spät?",
                new string[] { "Nein!", "Nope" }, new string[] { "Sag ich dir doch nicht, du Affe.", "Hmm, kp." },
                2);
                break;
            case 2:
                currentFrage = new Frage(
                "Wer, wie, was?",
                new string[] { "Wieso?", "Weshalb?", "Warum?" }, new string[] { "Darum!" },
                3);
                break;
            case 3:
                currentFrage = new Frage(
                "Was ist ein die Hälfte von einem Halben?",
                new string[] { "Hal" }, new string[] { "Glückwunsch", "Dreißig", "Spaßig" },
                1);
                break;
            case 4:
                currentFrage = new Frage(
                "Wer flog übers Kuckucksnest",
                new string[] { "Jack Nickolson", "Einer" }, new string[] { "Niemand.", "Weder noch" },
                2);
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
}
