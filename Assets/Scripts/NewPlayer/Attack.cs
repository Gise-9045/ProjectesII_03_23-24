using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] InputActionReference attack;
    [SerializeField] AudioClip screamClip;
    [SerializeField] AudioSource scream;
    [SerializeField, Range(0f, 1f)] private float volumeAudio = 0.2f;

    private bool isScreamPlaying = false;

    private void Awake()
    {
       

    }

    private void Update()
    {
        scream.clip = screamClip;
        scream.volume = volumeAudio;
        anim.SetBool("isAttacking", attack.action.ReadValue<float>() > 0);

        if (anim.GetBool("isAttacking") && !isScreamPlaying)
        {
            
            PlayScreamAudio();
        }
    }

    private void PlayScreamAudio()
    {
        scream.Play();
        isScreamPlaying = true;

        // Assuming scream.clip.length returns the length of the audio clip
        StartCoroutine(ResetScreamFlag(scream.clip.length));
    }

    private IEnumerator ResetScreamFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isScreamPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(1, true, 20.0f);
        }

        if (collision.tag == "Lever")
        {
            collision.GetComponent<leverActivation>().Toggle();
        }
    }
}
