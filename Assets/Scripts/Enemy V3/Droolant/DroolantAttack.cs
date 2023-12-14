using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DroolantAttack : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Transform parent;

    [SerializeField] private float cooldownAttack = 1.5f;
    float actualCooldownAttack;

    [SerializeField] private GameObject bullet;

    [SerializeField] Animator animator;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();

        actualCooldownAttack = 0;
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(parent.transform.position.x, parent.transform.position.y - 1);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down);

        Debug.DrawRay(pos, Vector2.down * hit.distance, Color.green);

        if(actualCooldownAttack > 0)
        {
            actualCooldownAttack -= Time.deltaTime;
            animator.SetBool("Shoot", false);

        }

        if (hit.collider.tag == "Player" && actualCooldownAttack <= 0)
        {
            animator.SetBool("Shoot", true);
            //Dispara
            Instantiate(bullet, pos, Quaternion.identity);

            actualCooldownAttack = cooldownAttack;
        }
    }
}
