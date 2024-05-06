using System;
using System.Collections;
using System.Collections.Generic;
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
public class ColorProperty
{
    public ColorTypes colorScheme;
    public Color color;
}


[Serializable]
public class ColorList
{
    public List<ColorProperty> colorList;
}

public class ColorSpreader : MonoBehaviour
{
    public ColorDatabase colorDatabase; // Referencia al ScriptableObject
    
    private Tilemap _spriteRenderer;

    public ColorTypes colorType; // Define el tipo de color a usar


    private void Start()
    {
        _spriteRenderer = GetComponent<Tilemap>();

        // Buscar el color correspondiente en la base de datos y asignarlo
        ColorProperty colorProperty = colorDatabase.colorList.Find(c => c.colorScheme == colorType);
        
        if (colorProperty != null)
        {
            _spriteRenderer.color = colorProperty.color;
        }
    }

    public Color GetColor()
    {
        return _spriteRenderer.color;
    }
    public ColorTypes GetColorType()
    {
        ColorProperty colorProperty = colorDatabase.colorList.Find(c => c.colorScheme == colorType);
        return colorProperty.colorScheme;
    }

}