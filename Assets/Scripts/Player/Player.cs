using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  

    [SerializeField] private int life;
    [SerializeField] int speed;

    [SerializeField] private bool dead;
    [SerializeField] public bool canDie; 
    [SerializeField] private bool stop;

    Vector2 direction;

    private AudioManager audioManager;
    [SerializeField] private GameObject crownGodMode;
    
    void Start()
    {
        direction= new Vector2(1, 1);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        canDie = true; 

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && !crownGodMode.activeSelf)
        {
            canDie = false;
            Debug.Log("GodMode");
            crownGodMode.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.G) && crownGodMode.activeSelf)
        {
            canDie = true;
            crownGodMode.SetActive(false);
        }


        if(Input.GetKeyDown(KeyCode.R)) { SceneArguments.SceneManager.LoadScene(SceneManager.GetActiveScene().name, "NoTransition"); }
    }

    public void SetLife(int l) { life = l; }
    public int GetLife() { return life; }
    public void SetSpeed(int s) { speed = s; }
    public int GetSpeed() { return speed; }
    public bool GetDead() { return dead; }

    public void SetStop(bool s) { stop = s; }
    public bool GetStop() { return stop; }

    public void TakeDamage()
    {
        if(canDie && !dead)
        {

            dead = true;
            StartCoroutine(Death());

        }
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

    

    private IEnumerator Death()
    {
        audioManager.PlaySFX(audioManager.death);
        yield return new WaitForSecondsRealtime(1f);
        SceneArguments.SceneManager.LoadScene(SceneManager.GetActiveScene().name, "NoTransition");
        dead = false;
    }
}
