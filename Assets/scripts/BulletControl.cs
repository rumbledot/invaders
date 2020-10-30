using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f;
    [SerializeField]
    private GameObject explosion;
    private GameObject activeBullets;

    // Start is called before the first frame update
    void Awake()
    {
        activeBullets = GameObject.FindGameObjectWithTag("Parent.Active.Bullet");
        transform.parent = activeBullets.transform;
        Rigidbody instBulletRB = gameObject.GetComponent<Rigidbody>();
        instBulletRB.AddForce(Vector3.left * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Recycle":
                Destroy(gameObject);
                break;
            case "Enemy":
                col.gameObject.GetComponent<EnemyControl>().takeDamage();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Recycle":
                Destroy(gameObject);
                break;
            case "Enemy":
                col.gameObject.GetComponent<EnemyControl>().takeDamage();
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
                Destroy(gameObject);
                break;
            case "Boss":
                col.gameObject.GetComponent<BossControl>().takeDamage();
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
