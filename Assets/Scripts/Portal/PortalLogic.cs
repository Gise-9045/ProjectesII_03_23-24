using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    public Transform PortalPosition;
    public bool isOrange;
    public float distance = 0.2f;
    private void Start()
    {
        if (!isOrange)
        {
            PortalPosition = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }
        else if(isOrange) 
        {
            PortalPosition = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Vector2.Distance (transform.position,collision.transform.position) > distance)
        {
            collision.transform.position = new Vector2(PortalPosition.position.x, PortalPosition.position.y);
        }
    }
}
