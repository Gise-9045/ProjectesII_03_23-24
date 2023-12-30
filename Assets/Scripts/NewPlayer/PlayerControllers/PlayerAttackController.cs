using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] AudioClip screamClip;
    [SerializeField] AudioSource scream;
    [SerializeField, Range(0f, 1f)] private float volumeAudio = 0.2f;
    [SerializeField] Animator anim;
    [SerializeField] private ParticleSystem attackParticles;
    private bool isScreamPlaying = false;

    private Enemy enemy;
    private Bullet bullet;
    private void Update()
    {
        scream.clip = screamClip;
        scream.volume = volumeAudio;
        if (anim.GetBool("isAttacking") && !isScreamPlaying)
        {

            PlayScreamAudio();
        }
        // NewInputManger._instance._playerAttackInput.action.ReadValue<float>() > 0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(1, true, 20.0f);
        }
    }

    public void Attack()
    {
        attackParticles.Play();
        anim.SetBool("isAttacking",true);
       
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

}
