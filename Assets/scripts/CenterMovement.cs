using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Update()
    {
        transform.position = new Vector3(5, -(player.position.y / 5), -(player.position.z / 5));
    }
}
