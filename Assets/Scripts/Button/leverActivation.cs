using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator pressed;

    private void Start()
    {
        isActive = false;
        sprite = GetComponent<SpriteRenderer>();
        pressed = GetComponent<Animator>();
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