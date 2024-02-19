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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //transitions.SetTrigger("LvlPassed");
        if (collision.CompareTag("Player"))
        {
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
                yield return new WaitForSeconds(0.7f);

                break;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("LvlPassed");

    }
}
