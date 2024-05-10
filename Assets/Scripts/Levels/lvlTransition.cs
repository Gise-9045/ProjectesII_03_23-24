using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlTransition : MonoBehaviour
{
    [SerializeField] public Scene currentScene;
    [SerializeField] private KeySaver keySaverList; // Referencia al script KeySaver

    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    private enum Transition { NONE, LEFT };
    [SerializeField] private Transition transition;

    private AudioManager audioManager; 
    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && keySaverList.GetListKeys().Count != 0)
        {
            audioManager.PlaySFX(audioManager.doorOpens);
            Time.timeScale = 0.0f;
            
            StartCoroutine(LevelTransition());
        }
       
    }


    IEnumerator LevelTransition()
    {
        switch (transition)
        {
            case Transition.LEFT:
                horizontal.SetActive(true);
                horizontalAnim.SetBool("LeftAnimation", true);
                yield return new WaitForSecondsRealtime(0.7f);

                break;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

    }
}
