using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField]
    private float life = 10f;
    [SerializeField]
    private float speed = 25f;
    private float oneBlock = 1f;
    private Vector3 moveToPosition;

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        life = life * GameControl.instance.healthFactorial;
        speed = speed * GameControl.instance.speedFactorial;
        CreateANewDestination();
        StartCoroutine(MoveAbout());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == moveToPosition)
        {
            CreateANewDestination();
        }
    }

    private void CreateANewDestination()
    {
        moveToPosition = new Vector3(0, Random.Range(-6, 6), Random.Range(-10, 10));
    }

    private IEnumerator MoveAbout()
    {
        float t = 0f;
        while (t < 1)
        {
            if (!GameControl.instance.isPaused)
            {
                t += Time.deltaTime / speed;
                transform.position = Vector3.Lerp(transform.position, moveToPosition, t);
                yield return null;
            }
        }
    }

    public void takeDamage()
    {
        life--;
        if (life <= 0)
        {
            GameControl.instance.addScore(50);
            GameControl.instance.setBosActive(false);
            GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            Destroy(gameObject);
        }
    }
}
