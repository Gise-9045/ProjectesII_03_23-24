using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject boxUI;
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

        if(!isActive)
        {
            boxUI.SetActive(true);

            // Llama a la funci�n HideBoxUI despu�s de 5 segundos
            Invoke("HideBoxUI", 5f);
        }

        isActive = true;
        

    }

    private void HideBoxUI()
    {
        // Desactiva el GameObject boxUI
        boxUI.SetActive(false);
    }
}