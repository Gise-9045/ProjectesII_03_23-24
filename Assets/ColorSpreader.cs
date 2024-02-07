using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public enum ColorTypes
{
    RED,
    GREEN,
    BLUE,
    PURPLE,
    PINK,
    NULL
}

[Serializable]
public class ColorProterty
{
    public ColorTypes colorScheme;
    public Color color;
}

[Serializable]
public class ColorList
{
    public List<ColorProterty> colorList;
}

public class ColorSpreader : MonoBehaviour
{
    public ColorProterty colorProperty;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = colorProperty.color;
    }

    public Color GetColor()
    {
        return colorProperty.color;
    }

}
