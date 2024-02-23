using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private enum Transition { NONE, UP, DOWN, LEFT, RIGHT};
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    [SerializeField] private Transition transition;

    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();


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

            default:
                break;
        
        }
    }

    void Update()
    {
        //if(verticalAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        //{
        //    verticalAnim.SetBool(verticalAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name, false);
        //}
    }
}
