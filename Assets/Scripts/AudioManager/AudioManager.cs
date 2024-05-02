
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private VolumeSettings volumeSettings;

    [Header("----- Audio Clip -----")]
    //public AudioClip music;
    public AudioClip death;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip fallToGround;
    public AudioClip dash;
    public AudioClip key;
    public AudioClip boxOpen;
    public AudioClip boxSliding;
    public AudioClip boxSurface;         
    public AudioClip doorOpens;         
    public AudioClip findKey;
    public AudioClip powerActive;
    public AudioClip stairsClimb;
    
    public AudioClip[] music;
    public AudioClip musicMainMenu;
    public AudioClip musicFall; 

    [Header("----- UI -----")]
    [SerializeField] private UnityEngine.UI.Image UISound;
    [SerializeField] private Sprite[] soundSprite;

    private bool inMainMenu; // This is a bool to check if the scene is the main menu again or not
    private int countMusic= 0;
    private int sceneCount = 0; 

    private bool isMenu = true;
    private bool isPlaying = false;
    private SceneManager currentScene;

    public AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        volumeSettings.StartVolumeSettings();

        countMusic = 0; 
        
        musicSource.clip = musicMainMenu; 
        musicSource.Play();
    }


    private void Update()
    {
        sceneCount = SceneManager.GetActiveScene().buildIndex;
        string sceneName = SceneManager.GetActiveScene().name;

        if (SceneManager.GetSceneByName("MainMenu").isLoaded && !isMenu)
        {
            musicSource.clip = musicMainMenu;
            musicSource.Play();
            isMenu = true;
        }
        else if (!SceneManager.GetSceneByName("MainMenu").isLoaded && isMenu)
        {
            if (SceneManager.GetSceneByName("Level 0").isLoaded)
            {
                ChangeMusic();
            }
            isMenu = false; 
        }
        
        
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
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX(AudioClip clip)
    {
        sfxSource.Stop();
    }

    public bool IsPlayingSFX()
    {
        return sfxSource.isPlaying;
    }

    public bool IsPlayingMusic()
    {
        return musicSource.isPlaying;
    }

    public void ChangeMusic()
    {
        if (countMusic >= 0)
        {
            if (countMusic >= music.Length)
            {
                countMusic = 0;
                musicSource.Stop();
                musicSource.clip = music[countMusic];
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                musicSource.clip = music[countMusic];
                countMusic += 1; 
                musicSource.Play();
            }
        }

    }
}

