using System.Collections;
using UnityEngine;
using TMPro;

public class touchPlatform : MonoBehaviour
{
    private float fallDelay = 0.5f;
    private float destroyDelay = 1f;
    private float respawnDelay = 3.5f;
    private float restartDelay = 5f;

    private float usedNumTouches;
    [SerializeField] private float numOfTouches = 4f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private GameObject player;
    private Vector3 initialPosition;
    private bool playerOnPlatform;
    public TextMeshProUGUI touchCounterText;
   

    private void Start()
    {
          
    usedNumTouches = numOfTouches;
        initialPosition = transform.position;
        touchCounterText.text = usedNumTouches.ToString();

        // Start the coroutine to reset touches every 5 seconds
        StartCoroutine(ResetTouchesPeriodically());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            usedNumTouches--;
            touchCounterText.text = usedNumTouches.ToString();

            if (usedNumTouches == 0)
            {
                StartCoroutine(Fall());
            }
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false; // Player is not colliding anymore
        }
    }
    private IEnumerator ResetTouchesPeriodically()
    {
        while (true)
        {
            // Check if the player is not colliding before resetting touches
            if (!playerOnPlatform)
            {
                usedNumTouches = numOfTouches;
                touchCounterText.text = usedNumTouches.ToString();
            }

            yield return new WaitForSeconds(restartDelay);
        }
    }

private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        touchCounterText.text = " ";
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(destroyDelay);
        yield return new WaitForSeconds(respawnDelay);
        RespawnPlatform();
        touchCounterText.text = " ";
        usedNumTouches = numOfTouches;
    }

    private void RespawnPlatform()
    {
        // Reset the position
        transform.position = initialPosition;

        // Enable the renderer and collider
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        rb.bodyType = RigidbodyType2D.Static;
        touchCounterText.text = numOfTouches.ToString();
    }

}
