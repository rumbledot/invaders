using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // instansiate this class
    public static GameControl instance;

    public GameObject player;

    // score element
    private Text scoreText;
    private int score;
    // UI objects
    public GameObject pausePanel;
    public GameObject scorePanel;
    public Text highScoreText;
    public TMP_InputField highScoreNameText;
    private Button pauseBtn;
    public Button submitScoreBtn;
    public Button restartBtn;
    public Button menuBtn;
    // game states
    public bool isPaused = false;
    private bool isBosActive = false;
    // important level governing object
    public GameObject spawnerManager;
    // difficulties default values
    private String[] diffLevelString = new string[] { "easy", "normal", "hard" };
    private new DiffcultyClass[] diffs = {
            new DiffcultyClass(1f, 1f, 1.2f),
            new DiffcultyClass(1.2f, 1.2f, 1f),
            new DiffcultyClass(1.4f, 1.4f, 0.8f)
    };
    // current difficulty variables
    private int diffLevel;
    public float healthFactorial;
    public float speedFactorial;
    public float timerFactorial;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        // check if UniversalManager instance exists
        // set difficulty level based on it or default to easy
        if (UniversalManager.instance)
        {
            SetDiffLevel();
        }
        else 
        {
            SetEasyDiff();
        }
    }

    void Start()
    {
        // get UI objects
        scoreText = GameObject.FindGameObjectWithTag("TEXT.SCORE").GetComponent<Text>();
        pauseBtn = GameObject.Find("button.pause").GetComponent<Button>();
        pauseBtn.onClick.AddListener(ShowPausePanel);
        restartBtn.onClick.AddListener(RestartTheGame);
        menuBtn.onClick.AddListener(BackToMenu);
        submitScoreBtn.onClick.AddListener(GetHighScoreName);
        // get player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckAndUpdateScore();
    }

    private void MakeInstance()
    {
        // check to instansiate this class
        if (instance == null)
        {
            instance = this;
        }
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

    private void RestartTheGame()
    {
        // restart level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // reset values
        spawnerManager.GetComponent<EnemySpawnerManager>().Restart();
        player.GetComponent<PlayerControl>().Restart();
        score = 0;
    }

    private void BackToMenu()
    {
        if (GameObject.FindGameObjectWithTag("Universal.manager").GetComponent<UniversalManager>().CheckHighScore(score))
        {
            isPaused = true;
            pausePanel.SetActive(false);
            scorePanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void GetHighScoreName()
    {
        var name = highScoreNameText.text;
        if (name == "") name = "new";
        GameObject.FindGameObjectWithTag("Universal.manager").GetComponent<UniversalManager>().addScore(name, score);
        SceneManager.LoadScene("Menu");
    }

    private void CheckAndUpdateScore()
    {
        if (score < 0)
        {
            ShowLoosePanel();
        }
        scoreText.text = score.ToString();
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

    private void SetDiffLevel()
    {
        diffLevel = UniversalManager.instance.getDiffLevel();
        healthFactorial = diffs[diffLevel].getHealthFactorial();
        speedFactorial = diffs[diffLevel].getSpeedFactorial();
        timerFactorial = diffs[diffLevel].getTimerFactorial();
    }
    private void SetEasyDiff()
    {
        diffLevel = 0;
        healthFactorial = 1f;
        speedFactorial = 1f;
        timerFactorial = 1.2f;
    }

    // external functions
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
}
