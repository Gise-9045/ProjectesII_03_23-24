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
    public AudioClip key;

    public AudioClip[] music;

    int countMusic = 0;
    [SerializeField] private TextMeshProUGUI nameMusic; 

    private void Start()
    {
        musicSource.clip = music[0];
        musicSource.Play();
        nameMusic.text = musicSource.clip.name;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(musicSource.isPlaying)
            {
                musicSource.Pause();
            }
            else
            {
                musicSource.Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            if(countMusic == music.Length-1)
            {
                musicSource.Stop();
                countMusic = 0;
                musicSource.clip = music[countMusic];
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                countMusic++;
                musicSource.clip = music[countMusic];
                musicSource.Play();
            }

            nameMusic.text = musicSource.clip.name;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (countMusic == 0)
            {
                musicSource.Stop();
                countMusic = music.Length - 1;
                musicSource.clip = music[countMusic];
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                countMusic--;
                musicSource.clip = music[countMusic];
                musicSource.Play();
            }

            nameMusic.text = musicSource.clip.name;
        }

       
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
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
