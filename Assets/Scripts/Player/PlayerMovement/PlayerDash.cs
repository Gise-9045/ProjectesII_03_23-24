using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    [SerializeField] private ParticleSystem dashTrail;
    private AudioManager audioManager;

    float actualDashCooldown;
    bool dashing = false;
    [SerializeField] float dashVelocity;
    float actualDashTimer;
    [SerializeField] float dashTimer;

    bool canDash;

    private PlayerPowerUpManager powerUpManager;



    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        powerUpManager = GetComponent<PlayerPowerUpManager>();

    }

    void Update()
    {
        if(powerUpManager.GetPowerUp() == ColorTypes.GREEN)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }
    }

    public void Dash()
    {
        if(canDash && !dashing && actualDashCooldown <= 0)
        {
            actualDashTimer = dashTimer;
            rb.gravityScale = 0f;
            audioManager.PlaySFX(audioManager.dash);
            dashTrail.Play();
        }
    }

    public void DashCheck()
    {
        if (actualDashCooldown > 0)
        {
            actualDashCooldown -= Time.deltaTime;
        }

        if (actualDashTimer > 0)
        {
            dashing = true;
            actualDashCooldown = 0.5f;
            rb.velocity = new Vector2(player.GetDirection().x * dashVelocity, 0);
            rb.gravityScale = 0f;
            actualDashTimer -= Time.deltaTime;

        }
        else
        {
            dashing = false;

        }
    }

    public bool GetIsDashing()
    {
        return dashing;
    }
}
