using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float knockbackVel;
    private bool knockback;

    [SerializeField] private int life;
    [SerializeField] int speed;
    [SerializeField] private ParticleSystem hurtParticles;
    
    Vector2 direction;

    [SerializeField] private Transform respawn; //posicion de respawn
    private Transform player;

    

    void Start()
    {
        direction= new Vector2(1, 1);
        player = GetComponent<Transform>();
    }

    public void SetLife(int l) { life = l; }
    public int GetLife() { return life; }
    public void SetSpeed(int s) { speed = s; }
    public int GetSpeed() { return speed; }

    public void TakeDamage()
    {
        
        hurtParticles.transform.position = player.transform.position;
        hurtParticles.Play();
        
        player.position = respawn.position;
    }

    public void SetDirection(Vector2 d)
    {
        //-1 izquierda 1 derecha
        direction = d;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction.x;
        currentScale.y = direction.y;
        gameObject.transform.localScale = currentScale;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }

    void Update()
    {
        
    }

   
}
