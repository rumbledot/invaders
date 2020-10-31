using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LBControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro[] playerInitials;
    [SerializeField]
    private TextMeshPro[] playerScores;

    void Start()
    {
        var listOfScores = UniversalManager.instance.getScores();
        listOfScores.Sort((p, q) => p.getScore().CompareTo(q.getScore()));
        for (int i = 0; i < 4; i++)
        {
            playerInitials[i].text = listOfScores[i].getName();
            playerScores[i].text = listOfScores[i].getScore().ToString();

        }
    }
}
