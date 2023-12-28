using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour
{
    public static HitParticles Instance { get; private set; }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    private void Awake()
    {
        Instance = this;
    }

    public void EnablePlayer(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y, -2);

        player.SetActive(true);

    }

    public void DisablePlayer()
    {

        player.SetActive(false);
    }




    public void EnableEnemy(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y, -2);

        enemy.SetActive(true);

    }

    public void DisableEnemy()
    {
        enemy.SetActive(false);
    }
}
