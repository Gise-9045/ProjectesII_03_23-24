using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ColorScheme
{
    RED,
    GREEN,
    BLUE,
    PURPLE,
    PINK,
    NULL
};
public class ColorPalette : MonoBehaviour
{
    public Color newColor;
    private SpriteRenderer myRenderer;
    public ColorScheme palette;
   

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        newColor.a = 1;
        myRenderer.color = newColor;
    }
    void Update()
    {
       
    }
    public ColorScheme GetPalette() { return palette; }
  
}

