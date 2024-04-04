using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    //[SerializeField] private AudioSource hurtSource;
    //[SerializeField] private AudioClip hurtClip;
    [SerializeField, Range(0f, 3f)] private float volumeAudio = 0.2f;
    [SerializeField] private int life;
    [SerializeField] private int maxLife;
    [SerializeField] private Vector2 direction;
    private float rotation;
    [SerializeField] int speed;

    private bool knockback;
    private float knockbackVel;

    private HitFlash flash;

    public bool GetKnockback()
    {
        return knockback;
    }

    public float GetKnockbackVel()
    {
        return knockbackVel;
    }

    private void Start()
    {
        //hurtSource.clip = hurtClip;
        life = maxLife;
        rotation = 0;
        SetDirection(direction);

        flash = GetComponentInChildren<HitFlash>();
    }

    public void SetLife(int l)
    {
        life = l;
    }
    public int GetLife()
    {
        return life;
    }

    public int GetMaxLife()
    {
        return maxLife;

    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetDirection(Vector2 d)
    {
        //-1 izquierda 1 derecha
        direction = d;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction.x;
        currentScale.y = direction.y;
        gameObject.transform.localScale = currentScale;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetRotation(float r)
    {
        rotation = r;

        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, rotation);

    }

    public float GetRotation()
    {
        return rotation;
    }

    public void TakeDamage(int subtractLife, bool knock, float knockVel)
    {
        life -= subtractLife;
       
        knockback = knock;
        knockbackVel = knockVel;

        flash.Flash(0.15f);
        HitParticles.Instance.DisableEnemy();
        HitParticles.Instance.EnableEnemy(gameObject.transform.position.x, gameObject.transform.position.y);

        

        if (knockback)
        {
            StartCoroutine(KnockBack());
        }

        if (life <= 0 )
        {
            //hurtSource.clip = hurtClip;
            CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
            HitStop.Instance.StopTime(0.15f, 0.5f);
            Destroy(gameObject);
            //hurtSource.Play();
        }
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage();
        }
    }

    private void Update()
    {
        //EnemyDirection
       // hurtSource.volume = volumeAudio;

        //EnemyRotation
    }
}
