using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    CameraZoom cam;
    [SerializeField] private HoleTransition holeTransition;

    private enum Transition { NONE, UP, DOWN, LEFT, RIGHT, INTROHOLE, HOLE};
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;
    [SerializeField] GameObject hole;

    private Animator verticalAnim;
    private Animator horizontalAnim;
    private Animator holeAnim;

    [SerializeField] private Transition transition;

    PlayerMovement player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        Time.timeScale = 1.0f;

        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
        holeAnim = hole.GetComponentInChildren<Animator>();

        cam = GameObject.Find("Main Camera").GetComponent<CameraZoom>();

        if(SceneArguments.SceneManager.GetSceneArguments() == "NoTransition")
        {
            return;
        }

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
        //cam.SetToPlayer();

        holeTransition.ResetToZero();
        //player.SetStop(true);

        holeTransition.Scale(4000);
        //cam.PlayerZoomOut();
        //player.SetStop(false);


        yield return new WaitForSeconds(3f);
        hole.SetActive(false);
    }


    public IEnumerator StartIntroHole()
    {
        hole.SetActive(true);
        cam.SetToPlayer();

        holeTransition.ResetToZero();
        player.SetActualState(PlayerMovement.PlayerStates.HANDUP);
        holeTransition.Scale(500);


        yield return new WaitForSeconds(2f);
        holeTransition.Scale(4000);
        cam.PlayerZoomOut();
        player.SetActualState(PlayerMovement.PlayerStates.IDLE);



        yield return new WaitForSeconds(3f);
        hole.SetActive(false);
    }

    void Update()
    {
        //if(verticalAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        //{
        //    verticalAnim.SetBool(verticalAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name, false);
        //}
    }
}
