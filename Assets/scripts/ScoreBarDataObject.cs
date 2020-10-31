using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScoreBar", order = 1)]
public class ScoreBarDataObject : ScriptableObject
{
    public string playerInitial;
    public int playerScore;
}
