using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBoxController : MonoBehaviour
{
    private AudioManager audioManager;
    public int countTouchGround = 0; 
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            audioManager.PlaySFX(audioManager.boxSurface); 
        }

        if(collision.gameObject.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.boxSliding);
            //hacer que se repita 


        }
    }
}
