using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UniversalManager : MonoBehaviour
{
    // instantiate this class
    public static UniversalManager instance;

    // difficulties variables
    private int diffLevel;
    private String[] diffLevelString =
        new String[] {
            "Easy", "Normal", "Hard"
        };
    // scores list and filename
    private List<ScoreClass> scores = new List<ScoreClass>();
    private const string savedDataFilename = "/InvadersHallOfFace.dat";

    void Awake()
    {
        MakeInstancePersistent();
        LoadScore();
    }

    // instantiate and keep this class alive from scene to scene
    private void MakeInstancePersistent()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // external functions
    public int getDiffLevel() 
    {
        return diffLevel;
    }
    public void setDiffLevel(int l)
    {
        diffLevel = l;
    }
    public String getDiffLevelString()
    {
        return diffLevelString[diffLevel];
    }
    public void addScore(string n, int s)
    {
        // add the new score
        ScoreClass newScore = new ScoreClass(n, s);
        scores.Add(newScore);
        // sort the scores list
        scores.Sort((q, p) => p.getScore().CompareTo(q.getScore()));
        // keep score list to 4 item
        if (scores.Count > 4)
        {
            scores.RemoveRange(4, scores.Count - 4);
        }

        SaveScores();
    }

    public Boolean CheckHighScore(int s)
    {
        if (s > scores[0].getScore())
        {
            return true;
        }
        return false;
    }

    public List<ScoreClass> getScores()
    {
        return scores;
    }

    // scores I/O
    private void SaveScores()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFilename))
        {
            File.Delete(Application.persistentDataPath + savedDataFilename);
        }
            BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savedDataFilename);
        bf.Serialize(file, scores);
        file.Close();
    }

    private void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFilename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savedDataFilename, FileMode.Open);
            var data = bf.Deserialize(file);
            file.Close();
            scores = (List<ScoreClass>)data;
        }
        else
        {
            scores = new List<ScoreClass>
            {
                new ScoreClass("aaa", 50),
                new ScoreClass("bbb", 40),
                new ScoreClass("ccc", 30),
                new ScoreClass("ddd", 10),
            };
        }
    }
}
