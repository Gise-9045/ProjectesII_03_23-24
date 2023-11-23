using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class laserBehaviour : MonoBehaviour
{
    public float Speed = 4.5f;
    
    [SerializeField] InputActionReference laser;
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
       
        
        transform.Translate(Vector2.right * Time.deltaTime * Speed);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
