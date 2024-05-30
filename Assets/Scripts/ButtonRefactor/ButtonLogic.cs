using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public enum ButtonStates { PUSH, TOGGLE }

    private BoxCollider2D col;
    private SpriteRenderer sr;

    [SerializeField] Sprite button;
    [SerializeField] Sprite buttonPush;

    private ButtonDetection bt;



    [SerializeField] private ButtonStates actualState;
    [SerializeField] private bool isActive;


    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        bt = GetComponentInChildren<ButtonDetection>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if(bt.GetButtonPush())
        {
            col.offset = new Vector2(0f, -0.21f);
            col.size = new Vector2(0.87f, 0.43f);
            sr.sprite = buttonPush;
            isActive = true;
        }
        else if(actualState == ButtonStates.TOGGLE)
        {
            sr.sprite = button;
            col.offset = new Vector2(0f, -0.09f);
            col.size = new Vector2(0.87f, 0.68f);
            isActive = false;
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}