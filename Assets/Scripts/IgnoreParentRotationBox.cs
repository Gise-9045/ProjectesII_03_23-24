using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentRotationBox : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
