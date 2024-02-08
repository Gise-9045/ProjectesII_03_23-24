using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OldPlayerDash : MonoBehaviour
{
    [SerializeField] private InputActionReference dash;
    private float isDashing;


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        isDashing = dash.action.ReadValue<float>();

    }

    private void FixedUpdate()
    {
        if(isDashing == 1)
        {
            rb.velocity = new Vector2(45, rb.velocity.y);
        }
    }
}
