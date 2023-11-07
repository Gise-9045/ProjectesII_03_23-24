using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    [SerializeField] private PlayerAttack controllerAtk;
    private bool playerIsHitting = false; // Track the previous state of the player's attack action
    public TextMeshProUGUI touchCounterText;
    public bool isActive;
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
