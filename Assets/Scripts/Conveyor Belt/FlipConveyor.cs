using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipConveyor : MonoBehaviour
{
    bool isActive;
    bool calledToggle;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        calledToggle = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            Toggle();
            
            
        }
    }

    void Toggle()
    {
        isActive = !isActive;
        calledToggle = true;
    }
    public bool GetIsActive() { return isActive; }
    public bool GetIsToggled() { return calledToggle; }
    public void SetisToggled(bool value) {  calledToggle = value; }
}
