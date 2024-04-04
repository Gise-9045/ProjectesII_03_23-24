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

    private InputController controller;

    private PlayerMovement colorCheck;

    private Player player;

    private Transform playerTr;

    private Rigidbody2D rb;
    private Transform tr;


    [SerializeField]private float playerOldPos = 0f;
    private Vector2 playerOldDirection;


    private bool picking;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        BlovkCollider = GetComponent<BoxCollider2D>();
        BlovkRenderer = GetComponentInChildren<SpriteRenderer>();
        BlovkParticles = GetComponentInChildren<ParticleSystem>();
        BlovkCollider.size = new Vector2 (BlovkRenderer.size.x, BlovkRenderer.size.y);

        playerDetection = GetComponentInChildren<PlayerDetection>();
        wallDetection = GetComponentInChildren<WallDetection>();
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
        colorCheck = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        //NOTA DE ADRIÁN PARA ADRIÁN
        //Hay que hacer getter de los PowerUps que hay por que es muy guarro pillar una variable publica de PlayerMovement >:(


        if(playerDetection.GetPlayerDetection() && controller.GetPowerUpKey() && colorCheck.canPickUp)
        {
            picking = true;
        }
        else if(controller.GetPowerUpKey() && picking && !wallDetection.GetWallDetection())
        {
            picking = false;
            Drop();
        }
        else if(!colorCheck.canPickUp)
        {
            picking = false;
            Drop();
        }


        float distance = Mathf.Sqrt(Mathf.Pow(tr.position.x - playerTr.position.x, 2) + Mathf.Pow(tr.position.y - playerTr.position.y, 2) + Mathf.Pow(tr.position.z - playerTr.position.z, 2));


        //if(wallDetection.GetWallDetection() && playerOldPos == 0f && picking)
        //{
        //    rb.gravityScale = 0f;
        //    playerOldPos = playerTr.position.x;
        //    playerOldDirection = player.GetDirection();
        //}

            //if(wallDetection.GetWallDetection() && playerOldDirection.x > 0 && playerTr.position.x < playerOldPos && picking)
            //{
            //    PickUp();
            //    rb.gravityScale = 0f;
            //}
            //else if(wallDetection.GetWallDetection() && playerOldDirection.x < 0 && playerTr.position.x > playerOldPos && picking)
            //{
            //    PickUp();
            //    rb.gravityScale = 0f;
            //}
            //else if(!wallDetection.GetWallDetection() && picking)
            //{
            //    PickUp();
            //    rb.gravityScale = 0f;
            //    playerOldPos = 0f;

            //}

            //if(distance > 1.5f && picking)
            //{
            //    picking = false;
            //    playerOldPos = 0f;
            //    Drop();
            //}



        if (picking)
        {
            PickUp();
            rb.gravityScale = 0f;
            playerOldPos = 0f;

        }

    }


    void PickUp()
    {
        tr.position = new Vector2(playerTr.position.x, playerTr.position.y + 1f);
    }

    void Drop()
    {
        //tr.position = new Vector2(player.position.x, player.position.y - 1f);
        //player.position = new Vector2(player.position.x, player.position.y + 1f);

        rb.gravityScale = 9.81f;
    }
}
