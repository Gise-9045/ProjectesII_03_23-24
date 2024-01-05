using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    RaycastHit2D rcGround;
    private Transform parent;

    [SerializeField] private Enemy enemy;

    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private float height;

    [SerializeField] private bool detectOnly;


    private bool onGround;


    void Start()
    {
        parent = GetComponentInParent<Transform>();
    }


    public bool OnGround()
    {
        return onGround;
    }

    private void Update()
    {
        Vector2 pos = new Vector2(parent.transform.position.x + (enemy.GetDirection().x * distanceX), parent.transform.position.y + distanceY);
        rcGround = Physics2D.Raycast(pos, Vector2.down, height);

        if (rcGround.collider != null && rcGround.collider.tag == "Ground")
        {
            Debug.DrawRay(pos, new Vector2(0, -height), Color.green);
            onGround = true;
        }
        else
        {
            Debug.DrawRay(pos, new Vector2(0, -height), Color.red);

            onGround = false;

            if (!detectOnly)
            {
                enemy.SetDirection(new Vector2(enemy.GetDirection().x * -1, 1));
            }
        }

        //Debug.Log(rcGround.collider.tag);

    }
}
