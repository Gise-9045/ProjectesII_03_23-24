using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intercaleLever : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator pressed;

    public void Start()
    {
        isActive = false;
        pressed = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }
    public void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            pressed.SetBool("Pressed", true);
            Toggle();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            pressed.SetBool("Pressed", false);
            Toggle();
        }
    }
    public void Toggle() { 
        isActive = !isActive;
       

    }
}
