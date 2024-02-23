using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")||collision.CompareTag("PuzzleBox"))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        isActive = !isActive;
        
    }
}
