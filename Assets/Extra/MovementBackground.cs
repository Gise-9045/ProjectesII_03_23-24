using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBackground : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector2 velocitySpeed; 
    private Vector2 offset;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        offset = new Vector2(velocitySpeed.x * Time.deltaTime, velocitySpeed.y * Time.deltaTime);
        material.mainTextureOffset += offset;
    }
}
