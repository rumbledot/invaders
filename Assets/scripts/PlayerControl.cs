using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    private Rigidbody rb;
    private Vector3 lastPos;

    // bullet related
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject shootFX;
    [SerializeField]
    private Transform nozzle;
    private bool canShoot = true;
    [SerializeField]
    private int shootTimer = 5;
    private int shootCounter = 0;

    [SerializeField]
    private GameObject batteryBar;
    private int energyBar = 0;
    private int energyBarMax = 500;
    private bool isShootingLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControl.instance.isPaused)
        {
            batteryBar.GetComponent<Slider>().value = energyBar;
            PlayerShooting();

            PlayerMovement();
        }
        if (isShootingLaser)
        {
            ShootingLaser();
        }
    }

    private void PlayerShooting()
    {
        if (!canShoot && !isShootingLaser)
        {
            shootCounter++;
        }
        if (shootCounter >= shootTimer)
        {
            shootCounter = 0;
            canShoot = true;
        }
        if (energyBar < energyBarMax && !isShootingLaser) {
            energyBar++;
        }

        if ((Input.GetKeyDown(KeyCode.Mouse0) ||
            Input.GetKeyDown(KeyCode.Space)) &&
            canShoot)
        {
            Instantiate(bullet, nozzle.position, Quaternion.Euler(0,0,90));
            shootCounter = 0;
            canShoot = false;
        }

        if ((Input.GetKeyDown(KeyCode.Mouse1) ||
            Input.GetKeyDown(KeyCode.LeftAlt)) &&
            energyBar > 0)
        {
            shootCounter = 0;
            canShoot = false;
            isShootingLaser = true;
            shootFX.SetActive(isShootingLaser);
        }

    }

    private void ShootingLaser()
    {
        energyBar -= 10;
        Instantiate(laser, nozzle.position + new Vector3(-1,0,0), Quaternion.Euler(0, 0, -90));
        if (energyBar <= 0)
        {
            isShootingLaser = false;
            energyBar = 0;
            shootFX.SetActive(isShootingLaser);
        }
    }

    private void PlayerMovement()
    {
        lastPos = rb.transform.position;

        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        if (Input.GetAxis("Vertical") > 0.0f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (Input.GetAxis("Vertical") < 0.0f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if ((transform.position.x > 30 || transform.position.x < 10) ||
            (transform.position.y > 6 || transform.position.y < -6) ||
            (transform.position.z > 10 || transform.position.z < -10))
        {
            transform.position = lastPos;
        }
    }

    public void Restart()
    {
        canShoot = true;
        transform.position = new Vector3(20, 0, 0);
        energyBar = 0;
    }
}
