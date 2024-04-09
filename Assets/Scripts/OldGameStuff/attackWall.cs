using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class attackWall : MonoBehaviour
{
    //[SerializeField] private Attack controllerAtk;
   
    public TextMeshProUGUI touchCounterText;
    public bool isActive;
    public void Start()
    {
        isActive = false;
        touchCounterText.text = "ATTACK ME";
    }

    public void Update()
    {

    }

    public void Toggle()
    {
        Destroy(gameObject);

    }

}
