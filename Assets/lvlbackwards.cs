using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlbackwards : MonoBehaviour
{
    public Animator transitions;
    [SerializeField] public Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //transitions.SetTrigger("LvlPassed");
       if(collision.gameObject.CompareTag("Player"))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
