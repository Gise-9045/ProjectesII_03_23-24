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

    [SerializeField] float arrayPosX;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();

        actualCooldownAttack = 0;
    }

    private void FixedUpdate()
    {
        Vector2 rayCastPos1 = new Vector2(parent.transform.position.x - arrayPosX, parent.transform.position.y - 1);
        Vector2 rayCastPos2 = new Vector2(parent.transform.position.x + arrayPosX, parent.transform.position.y - 1);

        RaycastHit2D hit = Physics2D.Raycast(rayCastPos1, Vector2.down);
        RaycastHit2D hit2 = Physics2D.Raycast(rayCastPos2, Vector2.down);

        Debug.DrawRay(rayCastPos1, Vector2.down * hit.distance, Color.green);
        Debug.DrawRay(rayCastPos2, Vector2.down * hit.distance, Color.green);

        if(actualCooldownAttack > 0)
        {
            actualCooldownAttack -= Time.deltaTime;
            animator.SetBool("Shoot", false);

        }

        if ((hit.collider.tag == "Player" || hit2.collider.tag == "Player") && actualCooldownAttack <= 0)
        {
            animator.SetBool("Shoot", true);
            //Dispara
            Vector2 bulletPos = new Vector2(parent.transform.position.x, parent.transform.position.y - 1);

            Instantiate(bullet, bulletPos, Quaternion.identity);
            actualCooldownAttack = cooldownAttack;
        }
    }
}
