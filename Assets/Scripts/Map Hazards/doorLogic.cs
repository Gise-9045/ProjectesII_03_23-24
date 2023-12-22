using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class doorLogic : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private leverActivation leverControl;
    
    [SerializeField] private SpriteRenderer sprite;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isActive = leverControl.isActive;
        
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        isActive = leverControl.isActive;
        GetComponent<Collider2D>().enabled = !isActive;
       sprite.enabled = !isActive;
        
    }
    public void Toggle()
    {
        isActive = !isActive;
       sprite.enabled = isActive;
        GetComponent<Collider2D>().enabled = isActive;
        
    }
}
