using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Transform tr;

    private Transform playerTr;

    private float actualZoom;
    private float finalZoom;
    private float velocity;
    private float smoothTime;

    private Vector2 actualPos;
    private Vector2 finalPos;

    void Start()
    {
        tr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        smoothTime = 0.25f;


        //Me guardo el zoom total de pantalla
        finalZoom = cam.orthographicSize;

        //Zoom inicial (Apuntando a player)
        cam.orthographicSize = 1;
        actualZoom = cam.orthographicSize;
        finalPos = Vector2.zero;

        tr.position = new Vector3(playerTr.position.x, playerTr.position.y, -20);
        actualPos = tr.position;

        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        yield return new WaitForSecondsRealtime(1f);
        actualZoom = finalZoom;
        actualPos = finalPos;
    }

    void Update()
    {
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, actualZoom, ref velocity, smoothTime);

        tr.position = new Vector3(Mathf.SmoothStep(tr.position.x, actualPos.x, 0.05f), Mathf.SmoothStep(tr.position.y, actualPos.y, 0.05f), -20);
    }
}
