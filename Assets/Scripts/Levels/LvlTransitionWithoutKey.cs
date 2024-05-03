using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlTransitionWithoutKey : MonoBehaviour
{
    [SerializeField] public Scene currentScene;

    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    private Animator doorAnim;

    private enum Transition { NONE, LEFT };
    [SerializeField] private Transition transition;

    private AudioManager audioManager;

    public bool activeSound = true; 


    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        doorAnim = GetComponentInChildren<Animator>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (activeSound)
            {
                //audioManager.PlaySFX(audioManager.doorOpens);
            }
            

            //Time.timeScale = 0.0f;
           
            //StartCoroutine(LevelTransition());

            doorAnim.SetBool("CloseDoor", true);
 
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
        //Debug.Log("LvlPassed");

    }
}
