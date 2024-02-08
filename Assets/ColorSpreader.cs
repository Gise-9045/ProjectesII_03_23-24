using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private Tilemap _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<Tilemap>();
        _spriteRenderer.color = colorProperty.color;
    }

    public Color GetColor()
    {
        return colorProperty.color;
    }

}
