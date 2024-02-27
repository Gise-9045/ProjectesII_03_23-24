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

    [SerializeField] private bool dead;

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
    public bool GetDead() { return dead; }

    public void TakeDamage()
    {
        dead = true;
        StartCoroutine(Death());
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

    private IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(1f);
        dead = false;
        player.position = respawn.position;
    }

    void Update()
    {
        
    }

   
}
