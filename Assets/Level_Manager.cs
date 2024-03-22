using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{

    [SerializeField] private GameObject LevelSelectorScreen;
    [SerializeField] private GameObject PauseMenuScreen; 
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    // Start is called before the first frame update
    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void LevelSelectorActive()
    {
       PauseMenuScreen.SetActive(false);
       LevelSelectorScreen.SetActive(true);
      
    }
    
    public void ExitLevelSelector()
    {
        PauseMenuScreen.SetActive(true);
        LevelSelectorScreen.SetActive(false);
    }
}
