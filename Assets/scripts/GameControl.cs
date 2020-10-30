﻿using System;
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

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        scoreText = GameObject.FindGameObjectWithTag("TEXT.SCORE").GetComponent<Text>();
        pauseBtn = GameObject.Find("button.pause").GetComponent<Button>();
        pauseBtn.onClick.AddListener(ShowPausePanel);
        restartBtn.onClick.AddListener(RestartTheGame);
        menuBtn.onClick.AddListener(BackToMenu);

        player = GameObject.FindGameObjectWithTag("Player");
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

    private void ShowPausePanel()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
    }
}
