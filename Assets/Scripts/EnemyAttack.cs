using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private string detectionTag = "Player";
    public PlayerStats player;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetBool("Attack", false);
            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(detectionTag))
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        Time.timeScale = 0;
        player.TakeDamage(1);
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1;
        col.enabled = false;

    }


    public void Attack()
    {
        col.enabled = true;
    }

    public void DisableAttack()
    {
        col.enabled = false;
    }

    public void StartAttacking()
    {
        anim.SetBool("Attack", true);
    }
}
