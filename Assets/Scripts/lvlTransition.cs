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
    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(keySaverList.GetListKeys().Count);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //transitions.SetTrigger("LvlPassed");
        if (collision.CompareTag("Player") && keySaverList.GetListKeys().Count != 0)
        {
            Time.timeScale = 0.0f;
            horizontalAnim.SetBool("ExitLeftAnimation", false);
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
        Debug.Log("LvlPassed");

    }
}
