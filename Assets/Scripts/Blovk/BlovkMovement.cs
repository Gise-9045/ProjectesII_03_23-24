using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlovkMovement : MonoBehaviour
{
    BoxCollider2D BlovkCollider;
    SpriteRenderer BlovkRenderer;
    ParticleSystem BlovkParticles;

    private PlayerDetection playerDetection;
    private WallDetection wallDetection;
    private WallDetection topDetection;

    private InputController controller;

    private PlayerPowerUpManager powerUpManager;

    private Player player;

    private Transform playerTr;

    private Rigidbody2D rb;
    private Transform tr;

    private Vector2 posVelocity;
    private float smoothTime;



    [SerializeField]private float playerOldPos = 0f;
    private Vector2 playerOldDirection;


    private bool picking = false;

    void Start()
    {
        posVelocity = Vector2.zero;
        smoothTime = 2f;



        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        BlovkCollider = GetComponent<BoxCollider2D>();
        BlovkRenderer = GetComponentInChildren<SpriteRenderer>();
        BlovkParticles = GetComponentInChildren<ParticleSystem>();
        BlovkCollider.size = new Vector2 (BlovkRenderer.size.x - 0.1f, BlovkRenderer.size.y - 0.1f);

        playerDetection = GetComponentInChildren<PlayerDetection>();

        wallDetection = gameObject.transform.Find("WallDetection").GetComponent<WallDetection>();
        topDetection = gameObject.transform.Find("TopDetection").GetComponent<WallDetection>();

        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
        powerUpManager = GameObject.FindWithTag("Player").GetComponent<PlayerPowerUpManager>();
    }

    void Update()
    {
        if(playerDetection.GetPlayerDetection() && !topDetection.GetWallDetection() && controller.GetPowerUpKey() && powerUpManager.GetPowerUp() == ColorTypes.PINK)
        {
            picking = true;
        }
        else if(controller.GetPowerUpKey() && picking)
        {
            //Arreglar!!!

            Vector2 oldBoxPos = tr.position;
            Vector2 oldPlayerPos = playerTr.position;

            tr.position = oldPlayerPos;
            playerTr.position = oldBoxPos;

            //NO BORRAR
/*            if(wallDetection.GetWallDetection())
            {
                Vector2 oldBoxPos = tr.position;
                Vector2 oldPlayerPos = playerTr.position;

                tr.position = oldPlayerPos;
                playerTr.position = oldBoxPos;
            }
            else
            {
                tr.position = new Vector2(player.GetDirection().x + playerTr.position.x, playerTr.position.y);
            }*/

            picking = false;
            Drop();
        }
        else if(powerUpManager.GetPowerUp() != ColorTypes.PINK)
        {
            picking = false;
            Drop();
        }



        float distance = Mathf.Sqrt(Mathf.Pow(tr.position.x - playerTr.position.x, 2) + Mathf.Pow(tr.position.y - playerTr.position.y, 2) + Mathf.Pow(tr.position.z - playerTr.position.z, 2));


        if (wallDetection.GetWallDetection() && playerOldPos == 0f && picking)
        {
            rb.gravityScale = 0f;
            playerOldPos = playerTr.position.x;
            playerOldDirection = player.GetDirection();
        }

        if (wallDetection.GetWallDetection() && playerOldDirection.x > 0 && playerTr.position.x < playerOldPos && picking)
        {
            PickUp();
            rb.gravityScale = 0f;
        }
        else if (wallDetection.GetWallDetection() && playerOldDirection.x < 0 && playerTr.position.x > playerOldPos && picking)
        {
            PickUp();
            rb.gravityScale = 0f;
        }
        else if (!wallDetection.GetWallDetection() && picking)
        {
            PickUp();
            rb.gravityScale = 0f;
            playerOldPos = 0f;

        }

        if (wallDetection.GetWallDetection() && distance > 1.5f && picking)
        {
            picking = false;
            playerOldPos = 0f;
            Drop();
        }



        //if (picking)
        //{
        //    PickUp();
        //    rb.gravityScale = 0f;
        //    playerOldPos = 0f;

        //}

    }


    void PickUp()
    {
        //tr.position = new Vector2(playerTr.position.x, playerTr.position.y + 1f);

        tr.position = new Vector3(Mathf.SmoothDamp(tr.position.x, playerTr.position.x, ref posVelocity.x, smoothTime * Time.deltaTime), Mathf.SmoothDamp(tr.position.y, playerTr.position.y + 1, ref posVelocity.y, smoothTime * Time.deltaTime), -0.02f);

        tr.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Drop()
    {
        rb.gravityScale = 9.81f;
    }
}
