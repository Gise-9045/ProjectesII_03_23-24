using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class laserBehaviour : MonoBehaviour
{
    public float Speed = 4.5f;
    private Enemy enemy;
    [SerializeField] InputActionReference laser;
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
       
        
        transform.Translate(Vector2.right * Time.deltaTime * Speed);

        StartCoroutine(DestroyLaser());
    }
    private IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
