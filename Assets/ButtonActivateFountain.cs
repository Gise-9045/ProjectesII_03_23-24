using TMPro;
using UnityEngine;

public class ButtonActivateFountain : MonoBehaviour
{
    public GrayZoneData grayDatabase;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject fountain;
    [SerializeField] private Animator pressed;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        pressed = GetComponent<Animator>();
        fountain.SetActive(false);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            pressed.SetBool("Pressed", true);
            fountain.SetActive(true);
           
        }
    }
   
}
