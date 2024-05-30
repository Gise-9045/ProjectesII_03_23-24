using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverActivateOnce : MonoBehaviour
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
    
    public void Toggle()
    {
        isActive = true;

    }
}
