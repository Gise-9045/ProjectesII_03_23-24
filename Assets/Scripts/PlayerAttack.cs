using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float isAttacking;
    public HashSet<GameObject> hitObjects = new HashSet<GameObject>();
    public bool isHitting = false;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private Transform sword;

    public Enemy enemyStats;

    void Start()
    {

    }
    void Update()
    {
        isAttacking = attack.action.ReadValue<float>();

        if (!isHitting)
            return;

        Collider2D[] collidersHit = Physics2D.OverlapCapsuleAll(sword.position + sword.up * 0.9f,
            new Vector2(0.4f, 1.8f),
            CapsuleDirection2D.Vertical,
            sword.rotation.eulerAngles.z);

        foreach (Collider2D collider in collidersHit)
        {
            Enemy e = collider.GetComponent<Enemy>();
            if (e != null)
            {
                bool valid = true;
                foreach (GameObject obj in hitObjects) {
                    valid &= !obj.Equals(e.gameObject);
                }
                if (valid)
                {
                    hitObjects.Add(e.gameObject);
                    e.TakeDamage(10, (Vector2)(e.transform.position - sword.transform.position) * 5f + Vector2.up * 2f);
                }
            }
        }
    }

    public void StartHit()
    {
        hitObjects.Clear();

        isHitting = true;
    }
    public void StopHit()
    {
        isHitting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(sword.position, sword.position + sword.up * 1.8f);
        Physics2D.OverlapCapsuleAll(sword.position,
            new Vector2(0.4f, 1.5f),
            CapsuleDirection2D.Vertical,
            sword.rotation.eulerAngles.z);
    }
}
