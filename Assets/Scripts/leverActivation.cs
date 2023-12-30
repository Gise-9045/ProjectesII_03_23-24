using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    [SerializeField] private Attacking controllerAtk;
    private bool playerIsHitting = false; //Track the previous state of the player's attack action
   
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

    public void Toggle()
    {
        isActive = true;
        sprite.flipX = isActive;
        
       
        
    }
}
