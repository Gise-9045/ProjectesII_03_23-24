using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class touchPlatform : MonoBehaviour
{

    private float fallDelay = 0.5f;
    private float destroyDelay = 1f;
    private float respawnDelay = 3.5f;
    private float restartDelay = 5f;
   
    public float usedNumTouches;
    [SerializeField] private float numOfTouches = 4f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject platformPrefab; // Assign the platform prefab in the Inspector
    [SerializeField] private GameObject player;
    private Vector3 initialPosition;
    public TextMeshProUGUI touchCounterText;

    private void Start()
    {
        usedNumTouches = numOfTouches;
        initialPosition = transform.position;
        touchCounterText.text = usedNumTouches.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            usedNumTouches--;
            touchCounterText.text = usedNumTouches.ToString();
         
            Debug.Log(usedNumTouches);
            if(usedNumTouches == 0)
            {
                StartCoroutine(Fall());
              
            }


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
