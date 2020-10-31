using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform center;
    [SerializeField]
    private Transform player;
    private float factor = 0.8f;
    private float turn_speed = 1f;
    private float camPosY, camPosZ;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(30, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        camPosY = -(player.position.y * factor);
        camPosZ = -(player.position.z * factor);
        transform.position = Vector3.MoveTowards(new Vector3(30, transform.position.y, transform.position.z), player.position, 0.3f);
        Quaternion _lookRotation =
            Quaternion.LookRotation((center.position - transform.position).normalized);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);
    }
}
