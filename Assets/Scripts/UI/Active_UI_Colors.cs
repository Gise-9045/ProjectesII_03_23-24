using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_UI_Colors : MonoBehaviour
{
    private Animator anim;
    private bool isActive;

    private Coroutine coroutine;
    [SerializeField] private float delay = 5f;

    private InputController controller;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        isActive = false;
    }

    private void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
    }

    private void Update()
    {
        if(controller.GetColorsMenu())
        {
            if(!isActive)
            {
                anim.SetBool("StartTab", true);
                coroutine = StartCoroutine(DisappearAfterDelay(delay));
                isActive = true;
            }
            else if (isActive)
            { 
                StopCoroutine(coroutine);
                anim.SetBool("StartTab", false); 
                isActive = false;
            }
        }
    }

    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        anim.SetBool("StartTab", false);
        isActive = false;
    }
}
