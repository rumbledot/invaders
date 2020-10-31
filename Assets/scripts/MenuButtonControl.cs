using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonControl : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;
    private MovePlaceholder player;

    void Awake()
    {
        start = transform.localScale;
        end = start * 1.2f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlaceholder>();
    }

    void OnMouseOver()
    {
        if (!player.getIsOpen())
        {
            gameObject.transform.localScale = Vector3.Lerp(end, start, 1f * Time.deltaTime);
        }
    }

    void OnMouseExit()
    {
        if (!player.getIsOpen())
        {
            gameObject.transform.localScale = Vector3.Lerp(start, end, 2f * Time.deltaTime);
        }
    }
}
