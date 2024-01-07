using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public PlayerData playerData;
    public int health;
    private int maxHealh;

    public bool knockback;
    public float knockbackVel;

    public bool hasJumpPowerUp;
    public bool hasDashPowerUp;
    public bool hasShoutPowerUp;

    public bool isDead;

    private Vector2 respawnPoint;

    private PlayerController playerController;

    private void Awake()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData scriptable object is not assigned!");
            return;
        }

        knockback = false;

        maxHealh = 3;
        playerData.health = maxHealh;

        respawnPoint = transform.position;

        playerController = GetComponent<PlayerController>();

    }

    public int GetHealth()
    {
        return playerData.health;
    }

    private void Update()
    {
        if (knockback && Time.timeScale == 1)
        {
            StartCoroutine(KnockBack());
        }
    }

    public void TakeDamage(int damage, float k, float direction)
    {

        playerData.health -= damage;

        knockbackVel = k;
        knockbackVel *= direction;

        HitParticles.Instance.DisablePlayer();
        HitParticles.Instance.EnablePlayer(gameObject.transform.position.x, gameObject.transform.position.y);

        knockback = true;
        CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
        HitStop.Instance.StopTime(0f, 0.5f);

        if (playerData.health <= 0)
        {
            playerData.health = 0;
            Time.timeScale = 1;
            playerController.ChangeState(PlayerController.PlayerStates.DEAD); 
        }
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }

    public void GodModeActivated()
    {
        hasJumpPowerUp = !hasJumpPowerUp; 
        hasDashPowerUp = !hasDashPowerUp;
        hasShoutPowerUp = !hasShoutPowerUp;
    }


    private void OnTriggerEnter2D(Collider2D collision) //colisionar con hazards
    {
        Debug.Log("colision tag on:" + collision.tag); 

        if (collision.CompareTag("Respawn"))
        {
            respawnPoint = transform.position;
        }

        if (collision.CompareTag("Hazards"))
        {
            health--;
            if (health == 0)
            {
                playerController.ChangeState(PlayerController.PlayerStates.DEAD);
            }
        }
    }

    public void Die()
    {
        //poner una pantalla en negro 
        playerController.playerInput.canMove = false; 
        playerController.playerInput.canJump = false; 

        transform.position = respawnPoint;
        health = maxHealh;
        playerData.health = maxHealh;
        playerData.hasJumpPowerUp = hasJumpPowerUp;
        playerData.hasDashPowerUp = hasDashPowerUp;
        playerData.hasShoutPowerUp = hasShoutPowerUp;
        playerData.respawnPoint = respawnPoint;
    }
}
