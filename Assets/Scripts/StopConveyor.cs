using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopConveyor : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private bool usedOnce = false;
    [SerializeField] private bool willBeUsedOnce;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            if (!usedOnce)
            {
                Toggle();
            }
            
        }
    }

    void Toggle()
    {
        isActive = !isActive;
        if(willBeUsedOnce)
        {
            usedOnce = true;
        }
    }
    public bool GetIsACtive() { return isActive; }
}
