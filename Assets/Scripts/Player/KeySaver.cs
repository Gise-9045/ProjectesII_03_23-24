using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class KeySaver : MonoBehaviour
{
    public List<Transform> keys = new List<Transform>(); // Lista para almacenar las llaves
    public List<Animator> keyAnim = new List<Animator>();

    private Transform playerTr;
    private Rigidbody2D playerRb;
    [SerializeField] private Player player;
    [SerializeField] private float distanceBetweenKeys = 2.0f;
    [SerializeField] private float smoothness = 5.0f; // Suavidad del movimiento de la llave
    private Quaternion playerPreviousRotation; // Rotaci�n anterior del jugador
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerTr = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            keys.Add(collision.transform);
            keyAnim.Add(collision.GetComponent<Animator>());
            audioManager.PlaySFX(audioManager.key);
        }
    }

    public List<Transform> GetListKeys() { return keys;}

    void Update()
    {
        // Movemos las llaves hacia el jugador con un espacio de 2 unidades entre ellas
        foreach (Transform key in keys)
        {
            Vector2 targetPosition = new Vector2(playerTr.position.x + player.GetDirection().x * distanceBetweenKeys * keys.IndexOf(key) + -player.GetDirection().x, playerTr.position.y + player.GetDirection().y * distanceBetweenKeys * keys.IndexOf(key) - 0.2f);
            key.position = Vector2.Lerp(key.position, targetPosition, Time.deltaTime * smoothness);
            key.localScale = new Vector2(player.GetDirection().x * 0.7f, 0.7f);
        }

        foreach (Animator keyAnim in keyAnim)
        {
            if(playerRb.velocity.x > 2f || playerRb.velocity.x < -2f)
            {
                keyAnim.SetBool("Jump", true);
            }
            else
            {
                keyAnim.SetBool("Jump", false);

            }
        }
    }
}