using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class laserBehaviour : MonoBehaviour
{
    private InputManager checkSides;
    private Vector2 oldPosition = Vector2.zero;
    public float speed = 4.5f;
    private Enemy enemy;
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private Move player;
    [SerializeField] InputActionReference laser;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {


        physics.velocity = new Vector2(oldPosition.x * speed, 0);


        StartCoroutine(DestroyLaser());
    }
   
    private IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(5,true,0);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        

    }
}
