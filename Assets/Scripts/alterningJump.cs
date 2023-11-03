using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class alterningJump : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerJump controllerJump;
    public TextMeshProUGUI touchCounterText;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (controllerJump.isJumping==true)
        {
            isActive = !isActive;
            Debug.Log("CAMBIO");
        }

        
        if (isActive == true)
        {
            GetComponent<Collider2D>().enabled = true;
            touchCounterText.text = ":)";
        }
        else if (isActive == false)
        {
            GetComponent<Collider2D>().enabled = false;
            touchCounterText.text = ":(";
        }

    }
}
