using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathPaintManager : MonoBehaviour
{
    public static DeathPaintManager Instance { get; private set; }
    private DeathPaintManager paintManager;
    
    [SerializeField] GameObject paintObject;
    [SerializeField] List<Vector3> paints;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(SceneArguments.SceneManager.GetSceneArguments() != "NoTransition")
        {
            //ELIMINA TODAS LAS MANCHAS DE PINTURA
        }
    }

    public void CreateDeathPaint(Vector3 pos)
    {
        paints.Add(pos);
        Instantiate(paintObject, pos, Quaternion.identity);
    }
}
