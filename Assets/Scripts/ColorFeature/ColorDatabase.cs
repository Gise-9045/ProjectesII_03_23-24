using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDatabase", menuName = "ColorManagement/ColorDatabase")]
public class ColorDatabase : ScriptableObject
{
    public List<ColorProperty> colorList; // Usa la clase corregida
}

