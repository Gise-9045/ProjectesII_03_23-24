using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject boxUI;

    private void Start()
    {
        isActive = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
       

        if(!isActive)
        {
            boxUI.SetActive(true);

            // Llama a la función HideBoxUI después de 5 segundos
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