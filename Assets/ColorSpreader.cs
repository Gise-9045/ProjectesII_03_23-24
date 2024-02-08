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
public class ColorProperty
{
    public ColorTypes colorScheme;
    public Color color;
}

[CreateAssetMenu(fileName = "ColorDatabase", menuName = "ColorManagement/ColorDatabase")]
public class ColorDatabase : ScriptableObject
{
    public List<ColorProperty> colorList; // Usa la clase corregida
}

[Serializable]
public class ColorList
{
    public List<ColorProperty> colorList;
}

public class ColorSpreader : MonoBehaviour
{
    public ColorDatabase colorDatabase; // Referencia al ScriptableObject
   [SerializeField] private Tilemap _spriteRenderer;
   
    public ColorTypes colorType; // Define el tipo de color a usar


    private void Start()
    {
       
        //_spriteRenderer = GetComponent<Tilemap>();
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

}
