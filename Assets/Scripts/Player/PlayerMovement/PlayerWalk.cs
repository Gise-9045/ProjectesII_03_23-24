using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    float slide;
    float actualWalkSoundDelay;
    PlayerGroundDetection ground;
    private AudioManager audioManager;
    private InputController controller;

    [SerializeField] private float walkSoundDelay;


    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        controller = GetComponent<InputController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {
 
    }

    public void Walk()
    {
        if (slide > 0)
        {
            slide -= Time.deltaTime;
        }
        else if (slide < 0)
        {
            slide = 0;
        }

        if (actualWalkSoundDelay < 0 && ground.OnGround() && (controller.GetMovement().x < 0 || controller.GetMovement().x > 0))
        {
            audioManager.PlaySFX(audioManager.walk);
            actualWalkSoundDelay = walkSoundDelay;
        }
        else
        {
            actualWalkSoundDelay -= Time.deltaTime;
        }


        if (controller.GetMovement().x < 0 || controller.GetMovement().x > 0)
        {
            slide = 0.1f;
            player.SetDirection(new Vector2(controller.GetMovement().x, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(player.GetDirection().x * (player.GetSpeed() * slide), rb.velocity.y);
        }
    }
}
