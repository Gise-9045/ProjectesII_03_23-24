using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchPlatform : MonoBehaviour
{

    private float fallDelay = 1f;
    private float destroyDelay = 1f;
    private float respawnDelay = 3.5f;
   
    public float usedNumTouches;
    [SerializeField] private float numOfTouches = 4f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private Collider2D collider;
    private Vector3 initialPosition;

    private void Start()
    {
        usedNumTouches = numOfTouches;
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usedNumTouches--;
            Debug.Log(usedNumTouches);
            if(usedNumTouches == 0)
            {
                StartCoroutine(Fall());
                usedNumTouches = numOfTouches;
            }
            
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);

        // Disable the renderer and collider instead of destroying the object
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);
        RespawnPlatform();
    }

    private void RespawnPlatform()
    {
        // Reset the position
        transform.position = initialPosition;

        // Enable the renderer and collider
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        rb.bodyType = RigidbodyType2D.Static;
    }
}
