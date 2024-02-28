using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_UI_Colors : MonoBehaviour
{
    private Animator anim;
    private bool isAcrive; 

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
                isAcrive = true;
            }
            else if (isAcrive)
            { 
                anim.SetBool("StartTab", false); 
                isAcrive = false;
            }
        }
    }
}
