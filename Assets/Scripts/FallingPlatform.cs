using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    private float respawnDelay = 5f; // Time to respawn the platform
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private Rigidbody2D rb;
     // Assign the platform prefab in the Inspector
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(destroyDelay);

        // Disable the renderer and collider instead of destroying the object
       

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
