using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StopTime(float scale, float delay)
    {
            StartCoroutine(HitTime(scale, delay));

    }


    private IEnumerator HitTime(float s, float d)
    {
        Time.timeScale = s;
        yield return new WaitForSecondsRealtime(d);
        Time.timeScale = 1;
    }
}
