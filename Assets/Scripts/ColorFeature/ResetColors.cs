using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColors : MonoBehaviour
{
    public GrayZoneData dataToReset;
    // Start is called before the first frame update
    void Start()
    {
        dataToReset.dashActive = false;
        dataToReset.jumpActive = false;
        dataToReset.carryActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
