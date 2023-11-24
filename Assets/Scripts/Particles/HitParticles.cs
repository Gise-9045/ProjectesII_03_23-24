using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour
{
    public static HitParticles Instance { get; private set; }

    [SerializeField] private GameObject[] particles;
    private void Awake()
    {
        Instance = this;
    }

    public void Enable(float x, float y)
    {
        gameObject.transform.position = new Vector2(x, y);

        for(int i = 0; i < particles.Length; i++) 
        {
            particles[i].SetActive(true);
        }
    }

    public void Disable()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false);
        }
    }
}
