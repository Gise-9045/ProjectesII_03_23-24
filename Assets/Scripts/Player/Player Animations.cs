using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Walk", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
    }
}
