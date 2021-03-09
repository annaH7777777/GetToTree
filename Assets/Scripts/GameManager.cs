using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] List<TextMeshProUGUI> nums;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI equationText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI instructionsText;
    [SerializeField] TextMeshProUGUI stepsText;
    [SerializeField] TextMeshProUGUI winText;
    public List<GameObject> people;
    public List<String> turnOrder = new List<String>();
    private static System.Random rng = new System.Random();
    public int curTurnOrderIndex = 0;
    public Button startButton;
    public Button restartButton;
    float currentTime = 0;
    public float startingTime = 60;
    bool gameStarted = false;
    public int stepsCount = 3;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another instance of GameManager already exists!");
        }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    internal void SetEquation(String s)
    {
        if (curTurnOrderIndex == 0)
            equationText.text = s + " =";
        else
            equationText.text = equationText.text + " " + s;
        if (curTurnOrderIndex < turnOrder.Count - 1)
        {
            curTurnOrderIndex++;
            turnText.text = turnOrder[curTurnOrderIndex] + "'s turn...";
        }
        else
            CheckEquation(equationText);
    }

    private void CheckEquation(TextMeshProUGUI equationText)
    {
        int d = 0;
        String[] equationNums = equationText.text.Split(' ');
        int c = int.Parse(equationNums[0]);
        int a = int.Parse(equationNums[2]);
        int b = int.Parse(equationNums[4]);
        if (equationNums[3] == "+")
            d = a + b;
        else if (equationNums[3] == "-")
            d = a - b;
        else if (equationNums[3] == "*")
            d = a * b;
        if (c == d)
        {
            turnText.text = "Congrats! go to next step";
            foreach(GameObject x in people)
            {
                Debug.Log(x.name + " GoNextDest");
                x.GetComponent<PersonScript>().GoNextDest();
            }
            stepsCount--;
            stepsText.text = "Steps left: " + stepsCount;
            DeleteNubmers();
            Randomize();
        }
           
        else
        {
            turnText.text = "Not corect! Try again this step";
            DeleteNubmers();
            Randomize();
        }
            
    }

    private void DeleteNubmers()
    {
        foreach(TextMeshProUGUI x in nums)
        {
            x.text = "";
        }
        curTurnOrderIndex = 0;
        equationText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        turnOrder.Add("Father");
        turnOrder.Add("Mother");
        turnOrder.Add("Son");
        turnOrder.Add("Daughter");
        currentTime = startingTime;
        timerText.text = "Time left: " + currentTime.ToString("0") + " sec.";
        stepsText.text = "Steps left: " + stepsCount;

    }
    public void EnableStartButton()
    {
        startButton.gameObject.SetActive(false);
        instructionsText.gameObject.SetActive(false);
        gameStarted = true;
    }
    public void EnableRestartButton()
    {
        restartButton.gameObject.SetActive(false);
        instructionsText.gameObject.SetActive(false);
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            currentTime -= 1 * Time.deltaTime;
            timerText.text = "Time left: " + currentTime.ToString("0") + " sec.";
            if (currentTime < 11)
                timerText.color = Color.red;
            if (currentTime < 0)
                currentTime = 0;
            if (stepsCount == 0)
            {
                winText.text = "Greetings! Family members found each other!";
                restartButton.gameObject.SetActive(true);
                turnText.gameObject.SetActive(false);
            }
                
            if (currentTime == 0 && stepsCount > 0)
            {
                winText.text = "You coundn't find each other, try again!";
                restartButton.gameObject.SetActive(true);
                turnText.gameObject.SetActive(false);
                //Start();
                //gameStarted = false;
                //stepsCount = 3;
                //currentTime = startingTime;
            }
            
        }
    }

    public void Randomize()
    {
        Shuffle(turnOrder);
        Shuffle(nums);
        for (int i = 0; i < 4; i++)
        {
            int a = UnityEngine.Random.Range(0, 3);
            if (a == 0)
                Sum();
            else if (a == 1)
                Subtract();
            else
                Multiply();
        }
        turnText.text = turnOrder[0] + "'s turn...";

    }

    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void Sum()
    {
        int a = UnityEngine.Random.Range(1, 51);
        int b = UnityEngine.Random.Range(1, 51);
        int c = a + b;
        SetNum(c.ToString(), 0);
        SetNum(a.ToString(), 1);
        SetNum('+'.ToString(), 2);
        SetNum(b.ToString(), 3);
    }

    private void SetNum(String c, int v)
    {
        foreach (TextMeshProUGUI s in nums)
        {
            if (String.IsNullOrEmpty(s.text) && s.CompareTag(turnOrder[v]))
            {
                Debug.Log("if is called ");
                s.text = c;
                break;
            }
        }
    }

    private void Multiply()
    {
        int a = UnityEngine.Random.Range(1, 11);
        int b = UnityEngine.Random.Range(1, 11);
        int c = a * b;
        SetNum(c.ToString(), 0);
        SetNum(a.ToString(), 1);
        SetNum('*'.ToString(), 2);
        SetNum(b.ToString(), 3);
    }

    private void Subtract()
    {
        int a = UnityEngine.Random.Range(51, 101);
        int b = UnityEngine.Random.Range(1, 51);
        int c = a - b;
        SetNum(c.ToString(), 0);
        SetNum(a.ToString(), 1);
        SetNum('-'.ToString(), 2);
        SetNum(b.ToString(), 3);
    }


    


}
