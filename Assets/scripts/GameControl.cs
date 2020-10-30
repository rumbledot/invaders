using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    private Text scoreText;
    private int score;
    public GameObject player;

    private Button pauseBtn;
    public GameObject pausePanel;
    public Button restartBtn;
    public Button menuBtn;

    public bool isPaused = false;

    public GameObject spawnerManager;
    public GameObject activeEnemies;
    public GameObject activeBullets;

    private bool isBosActive = false;

    [SerializeField]
    private Dropdown selectDiffcultyLevel;
    private int diffLevel;
    private String[] diffLevelString = 
        new String[] {
            "Easy", "Normal", "Hard"
        };
    public float healthFactorial;
    public float speedFactorial;
    public float timerFactorial;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        scoreText = GameObject.FindGameObjectWithTag("TEXT.SCORE").GetComponent<Text>();
        pauseBtn = GameObject.Find("button.pause").GetComponent<Button>();
        pauseBtn.onClick.AddListener(ShowPausePanel);
        restartBtn.onClick.AddListener(RestartTheGame);
        menuBtn.onClick.AddListener(BackToMenu);

        SetEasyDiff();

        selectDiffcultyLevel.onValueChanged.AddListener(delegate
        {
            GetSelectedDiffculty(selectDiffcultyLevel);
        });

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GetSelectedDiffculty(Dropdown dd)
    {
        switch (dd.value)
        {
            case 0:
                SetEasyDiff();
                break;
            case 1:
                SetNormalDiff();
                break;
            case 2:
                SetHardDiff();
                break;
            default:
                SetEasyDiff();
                break;
        }
    }

    private void SetHardDiff()
    {
        diffLevel = 2;
        healthFactorial = 1.4f;
        speedFactorial = 1.4f;
        timerFactorial = 0.8f;
    }

    private void SetNormalDiff()
    {
        diffLevel = 1;
        healthFactorial = 1.2f;
        speedFactorial = 1.2f;
        timerFactorial = 1f;
    }

    private void SetEasyDiff()
    {
        diffLevel = 0;
        healthFactorial = 1f;
        speedFactorial = 1f;
        timerFactorial = 1.2f;
    }

    private void BackToMenu()
    {
        Debug.Log("BACK TO MENU");
    }

    private void RestartTheGame()
    {
        if (activeEnemies.transform.childCount > 0)
        {
            for (int i = 0; i < activeEnemies.transform.childCount; i++)
            {
                Destroy(activeEnemies.transform.GetChild(i).gameObject);
            }
        }
        if (activeBullets.transform.childCount > 0)
        {
            for (int i = 0; i < activeBullets.transform.childCount; i++)
            {
                Destroy(activeBullets.transform.GetChild(i).gameObject);
            }
        }
        spawnerManager.GetComponent<EnemySpawnerManager>().Restart();
        player.GetComponent<PlayerControl>().Restart();
        score = 0;
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
        {
            ShowLoosePanel();
        }
        scoreText.text = score.ToString();
    }

    public void addScore(int point) 
    {
        score += point;
    }

    public void subScore(int point)
    {
        score -= point;
    }

    public void setBosActive(Boolean b)
    {
        isBosActive = b;
    }
    public Boolean getBosActive()
    {
        return isBosActive;
    }

    private void ShowPausePanel()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        if (isPaused)
        {
            GameObject.FindGameObjectWithTag("TEXT.PAUSE").GetComponent<Text>().text = "PAUSE on " + diffLevelString[diffLevel];
        }
    }

    private void ShowLoosePanel()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        if (isPaused)
        {
            GameObject.FindGameObjectWithTag("TEXT.PAUSE").GetComponent<Text>().text = "YOU LOOSE!";
        }
        RestartTheGame();
    }
}
