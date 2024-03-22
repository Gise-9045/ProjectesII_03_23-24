using System.Collections;
using UnityEngine;

public class Feedback_Key : MonoBehaviour
{
    [SerializeField] private Animator animKey;
    private AudioManager audioManager;
    [SerializeField] private float timeDelay = 1f;

    private bool isPlayingSound = false;
    private Coroutine soundCoroutine;

    private void Awake()
    {

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<KeySaver>().keys.Count == 0)
        {
            animKey.SetBool("TouchDoor", true);

            if (!isPlayingSound)
            {
                soundCoroutine = StartCoroutine(PlaySoundRepeatedly());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animKey.SetBool("TouchDoor", false);
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
            }
            isPlayingSound = false;
        }
    }

    IEnumerator PlaySoundRepeatedly()
    {
        isPlayingSound = true;

        while (true)
        {
            audioManager.PlaySFX(audioManager.findKey);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
