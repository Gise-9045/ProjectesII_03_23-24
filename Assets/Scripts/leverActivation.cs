using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private doorLogic doorAffected;

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
        if (collision.CompareTag("Player"))
        {
            Toggle();
            if (doorAffected.GetActive() == false)
            {
                doorAffected.Toggle();
            }
            
            
        }
    }
    public void Toggle()
    {
        isActive = true;
        
    }
}
