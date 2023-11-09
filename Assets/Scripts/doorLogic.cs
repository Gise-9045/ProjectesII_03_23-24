using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class doorLogic : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private leverActivation leverControl;
    public TextMeshProUGUI touchCounterText;
    // Start is called before the first frame update
    void Start()
    {
        isActive = leverControl.isActive;
        touchCounterText.text = isActive ? "OPEN" : "CLOSED";
    }
    private void FixedUpdate()
    {
        isActive = leverControl.isActive;
        GetComponent<Collider2D>().enabled = !isActive;
        touchCounterText.text = isActive ? "OPEN" : "CLOSED";
    }
    public void Toggle()
    {
        isActive = !isActive;
        GetComponent<Collider2D>().enabled = !isActive;
        touchCounterText.text = isActive ? "OPEN" : "CLOSED";
    }
}
