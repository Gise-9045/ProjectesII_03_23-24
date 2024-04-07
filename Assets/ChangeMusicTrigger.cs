using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusicTrigger : MonoBehaviour
{
    private AudioManager audioManager;
    private bool falling = false;
    private string lastSceneName; 
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        lastSceneName = PlayerPrefs.GetString("LastSceneName", "");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (lastSceneName != SceneManager.GetActiveScene().name)
            {
                audioManager.musicSource.Stop();
                audioManager.ChangeMusic();
                falling = false;
                Debug.Log("change music");
                lastSceneName = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("LastSceneName", lastSceneName);


            }
        }
    }
}