using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sp;

    private PlatformEffector2D platform;

    private Transform topPosition;

    private InputController controller;


    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();

        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
        topPosition = GetComponentInChildren<Transform>().Find("Top");

        platform = GetComponentInChildren<PlatformEffector2D>();

        col.size = new Vector2(sp.size.x/2, sp.size.y);

        topPosition.localPosition = new Vector2(0, sp.size.y/2);
    }

    void Update()
    {
        if (controller.GetMovement().y > 0 || controller.GetJumpKeyTap())
        {
            platform.rotationalOffset = 0f;
        }
        else if (controller.GetMovement().y < 0)
        {
            platform.rotationalOffset = 180f;
        }
    }
}
