using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    //public AudioClip music;
    public AudioClip death;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip key;
    public AudioClip boxOpen;
    public AudioClip boxSliding;
    public AudioClip boxSurface;         
    public AudioClip doorOpens;         
    public AudioClip findKey;
    public AudioClip powerActive;
    public AudioClip stairsClimb;
    
    public AudioClip music;

    [Header("----- UI -----")]
    [SerializeField] private UnityEngine.UI.Image UISound;
    [SerializeField] private Sprite[] soundSprite;

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
        UISound.sprite = soundSprite[0];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(musicSource.isPlaying)
            {
                musicSource.Pause();
                UISound.sprite = soundSprite[1];

            }
            else
            {
                musicSource.Play();
                UISound.sprite = soundSprite[0];
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX(AudioClip clip)
    {
        SFXSource.Stop();
    }

    public bool IsPlayingSFX()
    {
        return SFXSource.isPlaying;
    }

    public bool IsPlayingMusic()
    {
        return musicSource.isPlaying;
    }
}
