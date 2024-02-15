using TMPro;
using UnityEngine;

public class leverActivation : MonoBehaviour
{
    
    
   
    public bool isActive;
    [SerializeField] private SpriteRenderer sprite;
    private GameObject button;
    public void Start()
    {
        isActive = false;
        
        sprite = GetComponent<SpriteRenderer>();
        button = this.GetComponent<GameObject>();
    }

    public void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Toggle();
           button.SetActive(false);
        }
    }
    public void Toggle()
    {
        isActive = true;
        
    }
}
