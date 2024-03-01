using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    CameraZoom cam;

    private enum Transition { NONE, UP, DOWN, LEFT, RIGHT, INTROHOLE, HOLE};
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;
    [SerializeField] GameObject hole;

    private Animator verticalAnim;
    private Animator horizontalAnim;
    private Animator holeAnim;

    [SerializeField] private Transition transition;

    Player player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Time.timeScale = 1.0f;

        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
        holeAnim = hole.GetComponentInChildren<Animator>();

        cam = GameObject.Find("Main Camera").GetComponent<CameraZoom>();


        switch (transition) 
        { 
            case Transition.UP:
                vertical.SetActive(true);
                verticalAnim.SetBool("ExitUpAnimation", true);
                break;

            case Transition.DOWN:
                vertical.SetActive(true);
                verticalAnim.SetBool("ExitDownAnimation", true);
                break;

            case Transition.LEFT:
                horizontal.SetActive(true);
                horizontalAnim.SetBool("ExitLeftAnimation", true);
                break;

                case Transition.RIGHT:
                horizontal.SetActive(true);
                horizontalAnim.SetBool("ExitRightAnimation", true);

                break;

            case Transition.INTROHOLE:
                StartCoroutine(StartIntroHole());
                break;


            case Transition.HOLE:
                StartCoroutine(StartHole());
                break;

            default:
                break;
        
        }
    }

    public IEnumerator StartHole()
    {
        hole.SetActive(true);
        cam.SetToPlayer();


        //yield return new WaitForSeconds(0.5f);
        holeAnim.SetBool("OpenFromMid", true);
        cam.PlayerZoomOut();


        yield return new WaitForSeconds(3f);
        hole.SetActive(false);
        holeAnim.SetBool("OpenFromMid", false);
        holeAnim.SetBool("Open", false);
    }


    public IEnumerator StartIntroHole()
    {
        hole.SetActive(true);
        cam.SetToPlayer();
        holeAnim.SetBool("MidOpen", true);



        yield return new WaitForSeconds(1.5f);
        player.SetStop(true);

        yield return new WaitForSeconds(0.5f);
        holeAnim.SetBool("OpenFromMid", true);
        cam.PlayerZoomOut();
        player.SetStop(false);


        yield return new WaitForSeconds(3f);
        hole.SetActive(false);
        holeAnim.SetBool("MidOpen", false);
        holeAnim.SetBool("OpenFromMid", false);
    }



    void Update()
    {
        //if(verticalAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        //{
        //    verticalAnim.SetBool(verticalAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name, false);
        //}
    }
}
