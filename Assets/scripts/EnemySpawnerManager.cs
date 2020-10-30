using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject activeEnemies;
    [SerializeField]
    private List<Transform> spawners;
    [SerializeField]
    private GameObject[] enemies;
    
    [SerializeField]
    private int spawnTimer = 1200;
    private int spawnCounter = 0;
    private bool canSpawn = true;

    private int spawnerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            spawners.Add(gameObject.transform.GetChild(i));
            spawnerCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.isPaused)
        {
            SpawnerCounter();
            SpawnerManager();
        }
    }

    private void SpawnerCounter()
    {
        if (!canSpawn)
        {
            spawnCounter++;
            if (spawnCounter >= spawnTimer)
            {
                spawnCounter = 0;
                canSpawn = true;
            }
        }
    }

    private void SpawnerManager()
    {
        if (canSpawn)
        {
            for (int i = 0; i < spawnerCount; i++)
            {
                float shouldISpawn = Random.Range(0f, 4f);
                if (shouldISpawn > 2f && shouldISpawn < 3.5f)
                {
                    int whatToSpawn = (int)Random.Range(0, enemies.Length);
                    var newEnemy = Instantiate(enemies[whatToSpawn], spawners[i].position, Quaternion.identity);
                    newEnemy.transform.parent = activeEnemies.transform;
                }
            }
            canSpawn = false;
        }
    }

    public void Restart()
    {
        spawnCounter = 0;
        canSpawn = true;
    }
}
