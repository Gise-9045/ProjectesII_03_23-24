using System.Collections;
using TMPro;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    private float respawnDelay = 5f; // Time to respawn the platform
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private Rigidbody2D rb;
    public TextMeshProUGUI touchCounterText;
    // Assign the platform prefab in the Inspector
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        touchCounterText.text = "ABLE";
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
        touchCounterText.text = fallDelay.ToString();
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
        touchCounterText.text = "ABLE";

        rb.bodyType = RigidbodyType2D.Static;
    }
}
