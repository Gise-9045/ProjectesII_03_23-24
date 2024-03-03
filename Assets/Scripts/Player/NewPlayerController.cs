using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;

    public Transform platformCheck;
    public float platformCheckRadius;
    public LayerMask whatIsPlatform;
    private bool platform;

    void Start()
    {

    }

    //private void FixedUpdate() //fixedupdate è preferibile con tutto quello che ha a che fare con fisica
    //{
    //    Debug.Log("FixedUpdate");
    //    platform = Physics2D.OverlapCircle(platformCheck.position, platformCheckRadius, whatIsPlatform);

    //    if (Input.GetKeyDown(KeyCode.Space) && platform)
    //    {
    //        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
    //    }
    //    if (Input.GetKey(KeyCode.D) && platform)
    //    {
    //        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
    //        Debug.Log("D");
    //    }
    //    if (Input.GetKey(KeyCode.A) && platform)
    //    {
    //        GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
    //    }

    //    // flip player
    //    if (GetComponent<Rigidbody2D>().velocity.x > 0)
    //    {
    //        transform.localScale = new Vector3(1f, 1f, 1f);
    //    }
    //    else if (GetComponent<Rigidbody2D>().velocity.x < 0)
    //    {
    //        transform.localScale = new Vector3(-1f, 1f, 1f);
    //    }
    //}

}