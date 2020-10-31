using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private String[] diffLevelString = new string[] { "easy", "normal", "hard" };
    private new DiffcultyClass[] diffs = {
            new DiffcultyClass(1f, 1f, 1.2f),
            new DiffcultyClass(1.2f, 1.2f, 1f),
            new DiffcultyClass(1.4f, 1.4f, 0.8f)
    };
    public float healthFactorial;
    public float speedFactorial;
    public float timerFactorial;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        if (UniversalManager.instance)
        {
            SetDiffLevel();
        }
        else {
            SetEasyDiff();
        }
    }

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("TEXT.SCORE").GetComponent<Text>();
        pauseBtn = GameObject.Find("button.pause").GetComponent<Button>();
        pauseBtn.onClick.AddListener(ShowPausePanel);
        restartBtn.onClick.AddListener(RestartTheGame);
        menuBtn.onClick.AddListener(BackToMenu);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (score < 0)
        {
            ShowLoosePanel();
        }
        scoreText.text = score.ToString();
    }

    private void SetDiffLevel()
    {
        diffLevel = UniversalManager.instance.getDiffLevel();
        healthFactorial = diffs[diffLevel].healthFactorial;
        speedFactorial = diffs[diffLevel].speedFactorial;
        timerFactorial = diffs[diffLevel].timerFactorial;
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
        UniversalManager.instance.addScore(score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //if (activeEnemies.transform.childCount > 0)
        //{
        //    for (int i = 0; i < activeEnemies.transform.childCount; i++)
        //    {
        //        Destroy(activeEnemies.transform.GetChild(i).gameObject);
        //    }
        //}
        //if (activeBullets.transform.childCount > 0)
        //{
        //    for (int i = 0; i < activeBullets.transform.childCount; i++)
        //    {
        //        Destroy(activeBullets.transform.GetChild(i).gameObject);
        //    }
        //}
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
