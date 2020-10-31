using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Build.Player;
using UnityEngine;

public class UniversalManager : MonoBehaviour
{
    private const string savedDataFilename = "/InvadersHallOfFace.dat";
    public static UniversalManager instance;
    private int diffLevel;
    private String[] diffLevelString =
        new String[] {
            "Easy", "Normal", "Hard"
        };
    private List<int> scores;

    void Awake()
    {
        MakeInstancePersistent();
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeInstancePersistent()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

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
    public void addScore(int s)
    {
        scores.Add(s);

        SaveScores();
    }

    void SaveScores()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFilename))
        {
            File.Delete(Application.persistentDataPath + savedDataFilename);
        }
            BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savedDataFilename);
        bf.Serialize(file, scores);
        file.Close();
        Debug.Log("Game data saved!");
    }

    void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFilename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savedDataFilename, FileMode.Open);
            var data = bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!" + data);
        }
        else Debug.LogError("There is no save data!");
    }
}
