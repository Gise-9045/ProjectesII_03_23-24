using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusicTrigger : MonoBehaviour
{

    private AudioManager audioManager;
    private bool falling = false; 
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (SceneManager.GetSceneByName("Level 7").isLoaded)
        {
            if (collision.CompareTag("Player") && !falling)
            {
                audioManager.musicSource.Stop();
                audioManager.musicSource.clip = audioManager.musicFall;
                audioManager.musicSource.Play();
                falling = true;
            }
        }
        if (!SceneManager.GetSceneByName("Level 7").isLoaded)
        {
            if (collision.CompareTag("Player"))
            {
                audioManager.ChangeMusic();
                falling = false;
                Debug.Log("change music");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(GetComponent<Collider2D>());
        }
    }
}
