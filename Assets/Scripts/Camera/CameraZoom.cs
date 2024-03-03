using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private Transform tr;

    private Transform playerTr;

    private float actualZoom;
    private float finalZoom;
    private float zoomVelocity;
    private float smoothTime;

    private Vector2 actualPos;
    private Vector2 finalPos;
    private Vector2 posVelocity;

    void Awake()
    {
        cam = GetComponent<Camera>();
        tr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        smoothTime = 40f;
        zoomVelocity = 0;
        posVelocity = Vector2.zero;

        //Me guardo el zoom total de pantalla
        finalZoom = cam.orthographicSize;

        actualZoom = cam.orthographicSize;
        actualPos = tr.position;

        finalPos = Vector2.zero;
    }

    public void SetToPlayer()
    {
        //Set zoom and pos to player (Without movement)
        //Zoom inicial (Apuntando a player)
        cam.orthographicSize = 1;
        actualZoom = cam.orthographicSize;

        tr.position = new Vector3(playerTr.position.x, playerTr.position.y, -20);
        actualPos = tr.position;
    }

    public void PlayerZoomOut()
    {
        actualZoom = finalZoom;
        actualPos = finalPos;
    }

    void Update()
    {
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, actualZoom, ref zoomVelocity, smoothTime * Time.deltaTime);

        tr.position = new Vector3(Mathf.SmoothDamp(tr.position.x, actualPos.x, ref posVelocity.x, smoothTime * Time.deltaTime), Mathf.SmoothDamp(tr.position.y, actualPos.y, ref posVelocity.y, smoothTime * Time.deltaTime), -20);
    }
}
