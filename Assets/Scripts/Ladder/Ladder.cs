using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sp;

    private PlatformEffector2D platform;

    private Transform topPosition;

    Vector2 movementController;
    private InputSystem controller;

    private void Awake()
    {
        controller = new InputSystem();
        controller.Enable();
    }

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
        topPosition = GetComponentInChildren<Transform>().Find("Top");

        platform = GetComponentInChildren<PlatformEffector2D>();

        col.size = new Vector2(sp.size.x/2, sp.size.y);

        topPosition.localPosition = new Vector2(0, sp.size.y/2);
    }

    void Update()
    {
        movementController = controller.Player.Move.ReadValue<Vector2>();

        if(movementController.y > 0)
        {
            platform.rotationalOffset = 0f;
        }
        else if (movementController.y < 0)
        {
            platform.rotationalOffset = 180f;
        }
    }
}
