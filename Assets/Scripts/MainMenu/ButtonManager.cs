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


    [SerializeField] private Animator VerticalTransition;
    [SerializeField] private Animator HorizontalTransition;
    [SerializeField] private Animator StartSceneUp;

    //MEN� DE INICIO
    private void Start()
    {
        VerticalTransition.SetBool("ExitDownAnimation", true);
        StartSceneUp.SetBool("Down", true);

    }


    public void PlayButton()
    {
        VerticalTransition.SetBool("ExitDownAnimation", false);
        StartSceneUp.SetBool("Down", false);
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {

        VerticalTransition.SetBool("UpAnimation", true);
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
        HorizontalTransition.SetBool("LeftAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransition.SetBool("LeftAnimation", false);
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
        HorizontalTransition.SetBool("RightAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransition.SetBool("RightAnimation", false);
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
        HorizontalTransition.SetBool("LeftAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransition.SetBool("LeftAnimation", false);

    }


    public void BackControlsButton(GameObject currentScene)
    {
        //StartCoroutine(Settings2());
        currentScene.SetActive(false);
        options.SetActive(true);
    }


    IEnumerator Settings2()
    {
        HorizontalTransition.SetBool("RightAnimation", true);
        yield return new WaitForSeconds(0.5f);
        HorizontalTransition.SetBool("RightAnimation", false);

    }

}
