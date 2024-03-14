using System.Collections;
using UnityEngine;

public class reverseDoor : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private MonoBehaviour leverControl;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float timeDelay = 0.5f;
    private bool isToggling = false;

    void Start()
    {
        isActive = GetLeverIsActive();
        UpdateDoorState(); 
    }

    
    void Update()
    {
       
        if (GetLeverIsActive() != isActive && !isToggling)
        {
            if (leverControl is intercaleLever)
            {
                
                ToggleDoorImmediate();
            }
            else
            {
                StartCoroutine(ToggleWithDelay());
            }
        }
    }

    
    private void ToggleDoorImmediate()
    {
        isActive = !isActive;
        UpdateDoorState(); 
    }

    
    private IEnumerator ToggleWithDelay()
    {
        isToggling = true;
        yield return new WaitForSeconds(timeDelay); 
        isActive = !isActive;
        UpdateDoorState(); 
        isToggling = false;
    }

   
    private void UpdateDoorState()
    {
        GetComponent<Collider2D>().enabled = isActive;
        sprite.enabled = isActive;
    }

    // Method to get the lever's current state
    private bool GetLeverIsActive()
    {
        if (leverControl == null)
        {
            Debug.LogError("Lever control is not assigned!");
            return false;
        }

        if (leverControl is leverActivation)
        {
            return ((leverActivation)leverControl).isActive;
        }
        else if (leverControl is intercaleLever)
        {
            return ((intercaleLever)leverControl).isActive;
        }
        else
        {
            Debug.LogError("Unknown lever control type!");
            return false;
        }
    }
}
