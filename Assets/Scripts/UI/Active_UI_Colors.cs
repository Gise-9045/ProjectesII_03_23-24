using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_UI_Colors : MonoBehaviour
{
    private Animator anim;
    private bool isAcrive;

    private Coroutine coroutine;
    [SerializeField] private float delay = 5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isAcrive = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isAcrive)
            {
                anim.SetBool("StartTab", true);
                coroutine = StartCoroutine(DisappearAfterDelay(delay));
                isAcrive = true;
            }
            else if (isAcrive)
            { 
                StopCoroutine(coroutine);
                anim.SetBool("StartTab", false); 
                isAcrive = false;
            }
        }
    }

    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        anim.SetBool("StartTab", false);
        isAcrive = false;
    }
}
