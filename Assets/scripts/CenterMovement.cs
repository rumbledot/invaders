using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMovement : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameControl.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(5, -(player.position.y / 5), -(player.position.z / 5));
    }
}
