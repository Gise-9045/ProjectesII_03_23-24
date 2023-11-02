using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class alterningJump : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Rigidbody2D rb2d;
    GameObject playerChecker;
    public TextMeshProUGUI touchCounterText;
    // Start is called before the first frame update
    void Start()
    {
        playerChecker = GameObject.Find("-----Player");
        playerChecker.GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        
        

        
        if (isActive)
        {
            GetComponent<Collider2D>().enabled = true;
            touchCounterText.text = ":)";
        }
        else if (!isActive)
        {
            GetComponent<Collider2D>().enabled = false;
            touchCounterText.text = ":(";
        }

    }
}
