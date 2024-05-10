using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTransition : MonoBehaviour
{
    private Camera cam;
    private Transform tr;
    private Transform player;

    private float actualScale;
    private float ScaleVel;
    private float smoothTime;

    private float finalZoom;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        tr = GetComponent<Transform>();

        smoothTime = 30f;
        ScaleVel = 0;

        //Com és una esfera, tant la x com la y es la mateixa
        actualScale = 0;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        tr.position = cam.WorldToScreenPoint(player.localPosition);

        tr.localScale = new Vector3(Mathf.SmoothDamp(tr.localScale.x, actualScale, ref ScaleVel, smoothTime * Time.deltaTime), Mathf.SmoothDamp(tr.localScale.y, actualScale, ref ScaleVel, smoothTime * Time.deltaTime));
    }

    public void ResetToZero()
    {
        tr.localScale = Vector3.zero;
        actualScale = 0f;
    }

    public void Scale(float scale)
    {
        actualScale = scale;
    }
}
