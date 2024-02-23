using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrayZoneData", menuName = "GrayZone/GrayZoneDataBase")]

public class GrayZoneData : ScriptableObject
{
    public bool dashActive;
    public bool jumpActive;
    public bool carryActive;
}
