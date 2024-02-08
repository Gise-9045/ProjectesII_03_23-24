using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorPalette;

public class ColorSwap : MonoBehaviour
{
    public Color newColor;
    private SpriteRenderer myRenderer;
    public ColorScheme palette;
    
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponentInChildren<SpriteRenderer>();
        newColor.a = 1;
        myRenderer.color = newColor;

    }
    
    // Update is called once per frame
    void Update()
    {
        ChangeColor(palette);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChange"))
        {
            palette = collision.GetComponent<ColorSwap>().palette;
            Debug.Log("BEEP");
            ChangeColor(palette);
            Update();
        }
    }

    public void ChangeColor(ColorScheme palette)
    {
        switch (palette)
        {
            case ColorScheme.RED:
                newColor.r = 255;
                newColor.g = 0;
                newColor.b = 0;
                myRenderer.color = newColor;
                break;
            case ColorScheme.GREEN:
                newColor.r = 0;
                newColor.g = 255;
                newColor.b = 0;
                myRenderer.color = newColor;
                break;
            case ColorScheme.BLUE:
                newColor.r = 0;
                newColor.g = 0;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;
            case ColorScheme.PINK:
                newColor.r = 255;
                newColor.g = 0;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;
            case ColorScheme.PURPLE:
                newColor.r = 100;
                newColor.g = 0;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;
            case ColorScheme.NULL:
                newColor.r = 255;
                newColor.g = 255;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;

        }
    }
}
