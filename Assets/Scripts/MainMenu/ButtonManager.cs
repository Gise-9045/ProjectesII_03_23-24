using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject exitDialog;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject volumen;

    [SerializeField] private GameObject VerticalTransition;
    [SerializeField] private GameObject HorizontalTransition;

    private Animator VerticalTransitionAnim;
    private Animator HorizontalTransitionAnim;
    [SerializeField] private Animator StartSceneUp;

    void Awake()
    {
        VerticalTransition.SetActive(true);
        HorizontalTransition.SetActive(true);
    }


    //MEN� DE INICIO
    private void Start()
    {
        VerticalTransitionAnim = VerticalTransition.GetComponentInChildren<Animator>();
        HorizontalTransitionAnim = HorizontalTransition.GetComponentInChildren<Animator>();


        VerticalTransitionAnim.SetBool("ExitDownAnimation", true);
        StartSceneUp.SetBool("Down", true);

    }


    public void PlayButton()
    {
        VerticalTransitionAnim.SetBool("ExitDownAnimation", false);
        StartSceneUp.SetBool("Down", false);
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {

        VerticalTransitionAnim.SetBool("UpAnimation", true);
        StartSceneUp.SetBool("Up", true);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level 0");
    }

    public void SettingsButton()
    {
        //StartCoroutine(Settings());
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    IEnumerator Settings()
    {
        HorizontalTransitionAnim.SetBool("LeftAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransitionAnim.SetBool("LeftAnimation", false);
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void ExitButton()
    {
        exitDialog.SetActive(true);
    }



    //EXIT DIALOG
    public void Affirmative()
    {
        Application.Quit();
    }
    public void Negative()
    {
        exitDialog.SetActive(false);
    }



    //OPCIONES
    public void BackOptionsButton()
    {
        //StartCoroutine(Menu());
        mainMenu.SetActive(true);
        options.SetActive(false);
    }


    IEnumerator Menu()
    {
        HorizontalTransitionAnim.SetBool("RightAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransitionAnim.SetBool("RightAnimation", false);
        mainMenu.SetActive(true);
        options.SetActive(false);
    }


    //CONTROLS
    public void ControlsButton(GameObject newScene)
    {
        //StartCoroutine(Controls());
        newScene.SetActive(true);
        options.SetActive(false);
    }

    IEnumerator Controls()
    {
        HorizontalTransitionAnim.SetBool("LeftAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransitionAnim.SetBool("LeftAnimation", false);

    }


    public void BackControlsButton(GameObject currentScene)
    {
        //StartCoroutine(Settings2());
        currentScene.SetActive(false);
        options.SetActive(true);
    }


    IEnumerator Settings2()
    {
        HorizontalTransitionAnim.SetBool("RightAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransitionAnim.SetBool("RightAnimation", false);

    }

}
