using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Color newColor;
    private SpriteRenderer myRenderer;
    public ColorScheme palette;
    public enum ColorScheme
    {
        RED,
        GREEN, 
        BLUE,
        PURPLE,
        PINK,
        NULL
    };

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        newColor.a = 1;
        myRenderer.color = newColor;

    }
    void Update()
    {
        ChangeColor(palette);
    }
    public void ChangeColor(ColorScheme palette)
    {
        switch (palette)
        {
            case ColorScheme.RED:
                palette = ColorScheme.RED;
                newColor.r = 255;
                newColor.g = 0;
                newColor.b = 0;
                myRenderer.color = newColor;
                
                break;
            case ColorScheme.GREEN:
                palette = ColorScheme.GREEN;
                newColor.r = 0;
                newColor.g = 255;
                newColor.b = 0;
                myRenderer.color = newColor;
                 break;
            case ColorScheme.BLUE:
                palette = ColorScheme.BLUE;
                newColor.r = 0;
                newColor.g = 0;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;
            case ColorScheme.PINK:
                palette = ColorScheme.PINK;
                newColor.r = 255;
                newColor.g = 105;
                newColor.b = 108;
                myRenderer.color = newColor;
                break;
            case ColorScheme.PURPLE:
                palette = ColorScheme.PURPLE;
                newColor.r = 255;
                newColor.g = 0;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;
            case ColorScheme.NULL:
                palette = ColorScheme.NULL;
                newColor.r = 255;
                newColor.g = 255;
                newColor.b = 255;
                myRenderer.color = newColor;
                break;

        }
    }
}

