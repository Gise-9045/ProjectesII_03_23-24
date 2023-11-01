using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerJump controllerJump;

    public Animation anim; 

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        controllerJump = GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isMoving == false)
        {
            
        }
    }
}
