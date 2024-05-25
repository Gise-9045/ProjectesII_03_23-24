using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AudioBoxController : MonoBehaviour
{
    private AudioManager audioManager; 
    [SerializeField] private float timeDelay = 1f;
    private bool isPlayingSound = false;
    private Coroutine soundCoroutine;

    private InputController controller;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            audioManager.PlaySFX(audioManager.boxSurface);
        }

        if(collision.gameObject.tag == "Player" && (controller.GetMovement().x < 0 || controller.GetMovement().x > 0))
        {
            if (!isPlayingSound )
            {
                soundCoroutine = StartCoroutine(PlaySoundRepeatedly());
            }
        }

        if ((controller.GetMovement().x >= 0 || controller.GetMovement().x <= 0) && isPlayingSound)
        {
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
                audioManager.StopSFX(audioManager.boxSliding);
            }
            isPlayingSound = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
                audioManager.StopSFX(audioManager.boxSliding);
            }
            isPlayingSound = false;
        }
    }
    IEnumerator PlaySoundRepeatedly()
    {
        isPlayingSound = true;

        while (true)
        {   
            audioManager.PlaySFX(audioManager.boxSliding);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
