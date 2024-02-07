using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float knockbackVel;
    private bool knockback;

    [SerializeField] private int life;
    [SerializeField] int speed;

    int direction;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        InputController.OnJump += Jump;
        //InputController.OnMove 
    }

    private void OnDisable()
    {
        InputController.OnJump -= Jump;
    }


    public void SetLife(int l)
    {
        life = l;
    }
    public int GetLife()
    {
        return life;
    }

    public void TakeDamage(int subtractLife, bool knock, float knockVel)
    {
        life -= subtractLife;

        knockback = knock;
        knockbackVel = knockVel;

        HitParticles.Instance.DisableEnemy();
        HitParticles.Instance.EnableEnemy(gameObject.transform.position.x, gameObject.transform.position.y);



        if (knockback)
        {
            StartCoroutine(KnockBack());
        }

        if (life <= 0)
        {

            CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
            HitStop.Instance.StopTime(0.15f, 0.5f);
            Destroy(gameObject);

        }
    }

    public void SetDirection(int d)
    {
        //-1 izquierda 1 derecha
        direction = d;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }

    void Update()
    {
        
    }

    private void Jump()
    {
        Debug.Log("OWO");
    }
}
