using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class attackWall : MonoBehaviour
{
    [SerializeField] private PlayerAttackController controllerAtk;
    private bool playerIsHitting = false; // Track the previous state of the player's attack action
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
