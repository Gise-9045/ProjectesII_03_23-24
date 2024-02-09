using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
    RaycastHit2D rcGround;
    private Player parent;

    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private float height;

    private bool onGround;


    void Start()
    {
        parent = GetComponentInParent<Player>();
    }


    public bool OnGround()
    {
        return onGround;
    }

    private void Update()
    {
        Vector2 pos = new Vector2(0, 0);


        if(parent.GetDirection().x == 1)
        {
            pos = new Vector2(parent.transform.position.x + (1 * distanceX), parent.transform.position.y + distanceY);

        }
        else
        {
            pos = new Vector2(parent.transform.position.x + (-1 * distanceX), parent.transform.position.y + distanceY);

        }

        rcGround = Physics2D.Raycast(pos, Vector2.down, height);

        //if(rcGround.collider == null)
        //{
        //    Debug.Log("NULL");

        //}
        //else
        //{
        //    Debug.Log(rcGround.collider.tag);
        //}


        if (rcGround.collider != null && rcGround.collider.tag == "Ground" || rcGround.collider != null && rcGround.collider.tag == "ColorChange")
        {
            Debug.DrawRay(pos, new Vector2(0, -height), Color.green);
            onGround = true;
        }
        else
        {
            Debug.DrawRay(pos, new Vector2(0, -height), Color.red);

            onGround = false;
        }
    }
}
