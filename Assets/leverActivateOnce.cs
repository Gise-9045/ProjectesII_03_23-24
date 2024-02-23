using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverActivateOnce : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;

    public void Start()
    {
        isActive = false;

        sprite = GetComponent<SpriteRenderer>();

    }
    public void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            Toggle();
        }
    }
    
    public void Toggle()
    {
        isActive = true;

    }
}
