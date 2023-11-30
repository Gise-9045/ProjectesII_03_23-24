using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    [SerializeField] private Attack controllerAtk;
    private bool playerIsHitting = false; // Track the previous state of the player's attack action
    public TextMeshProUGUI touchCounterText;
    public bool isActive;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public void Start()
    {
        isActive = false;
        touchCounterText.text = isActive ? "ACTIVE" : "INACTIVE";
    }

    public void Update()
    {
        
    }

    public void Toggle()
    {
        isActive = !isActive;
        touchCounterText.text = isActive ? "ACTIVE" : "INACTIVE";
    }
}
