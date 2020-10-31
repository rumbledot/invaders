using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreClass
{
    private string name;
    private int score;

    public ScoreClass(string n, int s)
    {
        this.name = n;
        this.score = s;
    }

    public string getName() { return name; }
    public void setName(string n) { name = n; }
    public int getScore() { return score; }
    public void setScore(int s) { score = s; }

}
