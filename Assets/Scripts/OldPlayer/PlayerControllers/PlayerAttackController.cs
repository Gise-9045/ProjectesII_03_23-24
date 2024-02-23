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
        PlayScreamAudio();
        anim.SetBool("isAttacking",true);
       

    }
    private void PlayScreamAudio()
    {
        
        isScreamPlaying = true;
        scream.Play();
        // Assuming scream.clip.length returns the length of the audio clip
        StartCoroutine(ResetScreamFlag(scream.clip.length));
    }

    private IEnumerator ResetScreamFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isScreamPlaying = false;
        anim.SetBool("isAttacking", false);
    }

}
