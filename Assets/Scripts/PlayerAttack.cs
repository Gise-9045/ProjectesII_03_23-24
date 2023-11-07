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
    [SerializeField] private leverActivation leverLogic;
    private bool leverToggled = false; // Flag to prevent repeated toggles

    public Enemy enemyStats;

    void Start()
    {
        isHitting = false;
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

        bool leverDetected = false; // Flag to track lever detection in this frame

        foreach (Collider2D collider in collidersHit)
        {
            if (collider.CompareTag("Lever") && !leverToggled && !leverDetected)
            {
                leverLogic.Toggle();
                leverDetected = true;
                StartCoroutine(ResetLeverToggle());
            }
            Enemy e = collider.GetComponent<Enemy>();
            if (e != null)
            {
                bool valid = true;
                foreach (GameObject obj in hitObjects)
                {
                    valid &= !obj.Equals(e.gameObject);
                }
                if (valid)
                {
                    hitObjects.Add(e.gameObject);
                    e.TakeDamage(10, (Vector2)(e.transform.position - sword.transform.position) * 5f + Vector2.up * 2f);
                }
            }
            // Additional code for handling enemy collisions if needed
            isHitting = false;
            leverDetected = false;
        }
    }

    IEnumerator ResetLeverToggle()
    {
        leverToggled = true;
        yield return new WaitForSeconds(0.5f); // Adjust this cooldown duration as needed
        leverToggled = false;
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
    IEnumerator ResetCooldown()
    {
        leverToggled = true;
        yield return new WaitForSeconds(0.5f); // Adjust this cooldown duration as needed
        leverToggled = false;
    }
}
