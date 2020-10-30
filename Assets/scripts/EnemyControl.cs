using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    private int moveTimer = 300;
    private int moveCounter = 0;
    private float oneSquare = 2f;

    private Vector3[] movePattern;
    private int patternTimer = 60;
    private int patternCounter = 0;
    private int patternIndex = 0;

    [SerializeField]
    private int life = 1;

    private Component[] rends;
    private Color defaultColor;
    private int rendererCount = 0;
    private Color lerpedColor = Color.grey;
    private float lerpLevel = 0f;

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rends = gameObject.GetComponentsInChildren<Renderer>();
        defaultColor = GetComponentInChildren<Renderer>().material.color;
        foreach (Renderer r in rends)
        {
            r.material.color = lerpedColor;
        }

        movePattern = new Vector3[] 
        {
            new Vector3(0,0.5f,0),
            new Vector3(0,0,0.5f),
            new Vector3(0,-0.5f,0),
            new Vector3(0,0,-0.5f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.isPaused)
        {
            MovementCounter();
            MovingAbout();
        }
    }

    private void MovementCounter()
    {
        if (moveCounter < moveTimer)
        {
            moveCounter++;
        }
        else 
        {
            moveCounter = 0;
            MoveASquare();
        }
    }

    private void MoveASquare()
    {
        transform.position = new Vector3(transform.position.x + oneSquare, transform.position.y, transform.position.z);
        lerpLevel++;
        ChangeColor();
    }

    private void MovingAbout()
    {
        if (patternCounter < patternTimer)
        {
            patternCounter++;
        }
        else
        {
            patternCounter = 0;
            transform.position += movePattern[patternIndex];
            patternIndex++;
            if (patternIndex >= movePattern.Length)
            {
                patternIndex = 0;
            }
        }
    }

    void ChangeColor()
    {
        lerpedColor = Color.Lerp(Color.grey, defaultColor, lerpLevel / 5f);
        //for (int i = 0; i < rendererCount; i++)
        //{ 
        //    renderers[i].material.color = lerpedColor;
        //}
        foreach (Renderer r in rends)
        {
            r.material.color = lerpedColor;
        }
    }

    public void takeDamage()
    {
        life--;
        if (life <= 0)
        {
            GameControl.instance.addScore(10);
            GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Recycle.behind":
                GameControl.instance.subScore(50);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
