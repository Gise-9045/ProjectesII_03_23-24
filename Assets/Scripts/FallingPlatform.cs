using System.Collections;
using TMPro;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    private float respawnDelay = 3f; // Time to respawn the platform
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private Rigidbody2D rb;
    public TextMeshProUGUI touchCounterText;
    // Assign the platform prefab in the Inspector
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        touchCounterText.text = "Fall";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.y !> transform.position.y)
        {
            StartCoroutine(Fall());
            touchCounterText.text = "X(";
        }
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.position.y > transform.position.y)
        {
            // Disable the collider temporarily to allow the player to pass through
            
            GetComponent<Collider2D>().enabled = false;
        }
    }
   
        // Check if the player is above the platform
        
    
    private IEnumerator Fall()
    {
        
        yield return new WaitForSeconds(fallDelay);
        touchCounterText.text = "DESTROYED";
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        touchCounterText.text = " ";
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
        touchCounterText.text = "Fall";

        rb.bodyType = RigidbodyType2D.Static;
    }
}
