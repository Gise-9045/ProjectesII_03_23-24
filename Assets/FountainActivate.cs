using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainActivate : MonoBehaviour
{
    public GrayZoneData grayData;
    [SerializeField] private GameObject dashFountain;
    [SerializeField] private GameObject jumpFountain;
    [SerializeField] private GameObject pickFountain;
    // Start is called before the first frame update
    void Start()
    {
        //dashFountain.SetActive(false);
        //jumpFountain.SetActive(false);
        //pickFountain.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        SetFountainToTrue();
    }
    public void SetFountainToTrue()
    {
        if (grayData.dashActive)
        {
            dashFountain.SetActive(true);
        }
        if (grayData.jumpActive)
        {
            jumpFountain.SetActive(true);
        }
        if (grayData.carryActive)
        {
            pickFountain.SetActive(true);
        }
    }
}
