using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Screen_Settings : MonoBehaviour
{
    
    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
