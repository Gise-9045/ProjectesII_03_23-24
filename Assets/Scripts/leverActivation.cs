using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    [SerializeField] private Attack controllerAtk;
    private bool playerIsHitting = false; // Track the previous state of the player's attack action
    public TextMeshProUGUI touchCounterText;
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    public void Start()
    {
        isActive = false;
        touchCounterText.text = isActive ? "ACTIVE" : "INACTIVE";
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        
    }

    public void Toggle()
    {
        isActive = !isActive;
        sprite.flipX = isActive;
        touchCounterText.text = isActive ? "ACTIVE" : "INACTIVE";
       
        
    }
}
