using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Sprite Heart0;
    [SerializeField] private Sprite Heart1;
    [SerializeField] private Sprite s;

    PlayerStats playerStats;
    private int health;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        health = playerStats.GetHealth();

        switch (health)
        {
            case 3:
                hearts[0].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                hearts[1].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                hearts[2].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                break;

            case 2:
                hearts[0].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                hearts[1].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                hearts[2].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                break;

            case 1:
                hearts[0].GetComponent<UnityEngine.UI.Image>().sprite = Heart1;
                hearts[1].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                hearts[2].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                break;

            case 0:
                hearts[0].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                hearts[1].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                hearts[2].GetComponent<UnityEngine.UI.Image>().sprite = Heart0;
                break;

            default:
                break;
        }
    }
}
