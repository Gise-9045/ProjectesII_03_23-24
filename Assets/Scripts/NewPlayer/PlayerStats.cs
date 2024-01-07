using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{

    public int health;
    private int maxHealth;

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
        if (FindObjectsOfType<PlayerStats>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        knockback = false;

        maxHealth = 3;
        health = PlayerPrefs.GetInt("PlayerHealth", health); 
        maxHealth = PlayerPrefs.GetInt("MaxPlayerHealth", maxHealth);

        respawnPoint = new Vector2(PlayerPrefs.GetFloat("RespawnPointX", transform.position.x),
                                   PlayerPrefs.GetFloat("RespawnPointY", transform.position.y));

        hasJumpPowerUp = PlayerPrefs.GetInt("HasJumpPowerUp", 0) == 1; 
        hasDashPowerUp = PlayerPrefs.GetInt("HasDashPowerUp", 0) == 1;
        hasShoutPowerUp = PlayerPrefs.GetInt("HasShoutPowerUp", 0) == 1;

        playerController = GetComponent<PlayerController>();
    }
    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.SetInt("MaxPlayerHealth", maxHealth);
        PlayerPrefs.SetFloat("RespawnPointX", respawnPoint.x);
        PlayerPrefs.SetFloat("RespawnPointY", respawnPoint.y);

        PlayerPrefs.SetInt("HasJumpPowerUp", hasJumpPowerUp ? 1 : 0);
        PlayerPrefs.SetInt("HasDashPowerUp", hasDashPowerUp ? 1 : 0);
        PlayerPrefs.SetInt("HasShoutPowerUp", hasShoutPowerUp ? 1 : 0);

        PlayerPrefs.Save();
    }

    public int GetHealth()
    {
        return health;
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

        health -= damage;
        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.Save();
        knockbackVel = k;
        knockbackVel *= direction;

        HitParticles.Instance.DisablePlayer();
        HitParticles.Instance.EnablePlayer(gameObject.transform.position.x, gameObject.transform.position.y);

        knockback = true;
        CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
        HitStop.Instance.StopTime(0f, 0.5f);

        if (health <= 0)
        {
            health = 0;
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
        playerController.playerInput.canMove = false;
        playerController.playerInput.canJump = false;

        transform.position = respawnPoint;
        health = maxHealth;
        respawnPoint = transform.position;

        SavePlayerPrefs(); // Save player preferences on death
    }
}