using UnityEngine;

public class doorLogic : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private MonoBehaviour leverControl; // Change the type to MonoBehaviour
    [SerializeField] private SpriteRenderer sprite;
    //[SerializeField] private AudioClip doorClip;
    //[SerializeField] private AudioSource doorSource;
    //[SerializeField, Range(0f, 3f)] private float volumeAudio = 0.2f;
    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
       
        isActive = GetLeverIsActive();
        //sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //doorSource.volume = volumeAudio;    
        isActive = GetLeverIsActive();
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

    

    private bool GetLeverIsActive()
    {
        if (leverControl == null)
        {
            Debug.LogError("Lever control is not assigned!");
            return false;
        }

        if (leverControl is leverActivation)
        {
            return ((leverActivation)leverControl).isActive;
        }
        else if (leverControl is leverActivateOnce)
        {
            return ((leverActivateOnce)leverControl).isActive;
        }
        else
        {
            Debug.LogError("Unknown lever control type!");
            return false;
        }
    }
}
