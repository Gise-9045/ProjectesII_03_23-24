using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [SerializeField] private GameObject doorFrame;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject blackSquare;

    private Animator doorAnim;


    private AudioManager audioManager; 
    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        doorAnim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        
    }


    IEnumerator LevelTransition()
    {
        switch (transition)
        {
            case Transition.LEFT:
                yield return new WaitForSecondsRealtime(0.3f);
                doorAnim.SetBool("OpenDoor", false);
                doorAnim.SetBool("CloseDoor", true);

                yield return new WaitForSecondsRealtime(0.5f);

                

                horizontal.SetActive(true);
                horizontalAnim.SetBool("LeftAnimation", true);
                yield return new WaitForSecondsRealtime(0.7f);

                break;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseDoor()
    {
        StartCoroutine(LevelTransition());
    }

    public void OpenDoor()
    {
        doorAnim.SetBool("OpenDoor", true);
        audioManager.PlaySFX(audioManager.doorOpens);
    }

    public void ShowBlackSquare(float pos, bool flip)
    {
        blackSquare.transform.localPosition = new Vector2(pos, blackSquare.transform.localPosition.y);

        if(flip)
        {
            blackSquare.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            blackSquare.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        
        blackSquare.SetActive(true);
    }
}
