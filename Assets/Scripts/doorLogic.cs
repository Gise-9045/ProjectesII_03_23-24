using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class doorLogic : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private leverActivation leverControl;
    [SerializeField] private SpriteRenderer sprite;
    //[SerializeField] private AudioClip doorClip;
    //[SerializeField] private AudioSource doorSource;
    //[SerializeField, Range(0f, 3f)] private float volumeAudio = 0.2f;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isActive = leverControl.isActive;
        
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        //doorSource.volume = volumeAudio;    
        isActive = leverControl.isActive;
        GetComponent<Collider2D>().enabled = !isActive;
       sprite.enabled = !isActive;
        
    }
    public void Toggle()
    {
        isActive = !isActive;
        //doorSource.clip = doorClip;
       sprite.enabled = isActive;
        //doorSource.Play();
        GetComponent<Collider2D>().enabled = isActive;
        
    }
}
