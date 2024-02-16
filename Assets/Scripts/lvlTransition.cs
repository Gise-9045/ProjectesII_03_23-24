using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlTransition : MonoBehaviour
{
    public Animator transitions;
    [SerializeField] public Scene currentScene;
    [SerializeField] private KeySaver keySaverList; // Referencia al script KeySaver
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(keySaverList.GetListKeys().Count);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //transitions.SetTrigger("LvlPassed");
        if (collision.CompareTag("Player") && keySaverList.GetListKeys().Count != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("LvlPassed");
        }
       
    }
}
