using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlaceholder : MonoBehaviour
{
    private Vector3 lastPos;
    [SerializeField]
    private GameObject playPanel;
    [SerializeField]
    private GameObject diffEasy;
    [SerializeField]
    private GameObject diffNormal;
    [SerializeField]
    private GameObject diffHard;
    [SerializeField]
    private Text playPanelInfo;
    [SerializeField]
    private Button buttonYes;
    [SerializeField]
    private Button buttonNo;
    private bool isOpen = false;
    private bool isExpand = false;

    private void Start()
    {
        buttonYes.onClick.AddListener(GoOnPlaying);
        buttonNo.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        playPanel.SetActive(false);
        isOpen = false;
    }

    private void GoOnPlaying()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                switch (hit.transform.name)
                {
                    case "menu.play":
                        playPanel.SetActive(true);
                        playPanelInfo.text = "play game on " + UniversalManager.instance.getDiffLevelString() + " ?";
                        isOpen = true;
                        break;
                    case "menu.diffs":
                        toggleDiffMenus();
                        break;
                    case "menu.diff.easy":
                        UniversalManager.instance.setDiffLevel(0);
                        toggleDiffMenus();
                        break;
                    case "menu.diff.normal":
                        UniversalManager.instance.setDiffLevel(1);
                        toggleDiffMenus();
                        break;
                    case "menu.diff.hard":
                        UniversalManager.instance.setDiffLevel(2);
                        toggleDiffMenus();
                        break;
                    case "menu.lb":
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Move()
    {
        if (!isOpen)
        {
            lastPos = transform.position;

            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                transform.position = new Vector3(20, hit.point.y, hit.point.z);

                if ((transform.position.x > 30 || transform.position.x < 10) ||
                (transform.position.y > 4 || transform.position.y < -4) ||
                (transform.position.z > 8 || transform.position.z < -8))
                {
                    transform.position = lastPos;
                }
            }
        }
    }

    public Boolean getIsOpen() 
    {
        return isOpen;
    }

    public void toggleDiffMenus()
    {
        isExpand = !isExpand;
        diffEasy.SetActive(isExpand);
        diffNormal.SetActive(isExpand);
        diffHard.SetActive(isExpand);
    }
}
