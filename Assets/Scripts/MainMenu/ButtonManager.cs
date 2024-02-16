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


    [SerializeField] private Animator VerticalTransition;
    [SerializeField] private Animator HorizontalTransition;
    [SerializeField] private Animator StartSceneUp;

    //MENÚ DE INICIO
    private void Start()
    {

    }


    public void PlayButton()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        VerticalTransition.SetBool("UpAnimation", true);
        StartSceneUp.SetBool("Up", true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level Tutorial");
    }

    public void SettingsButton()
    {
        StartCoroutine(Settings());
    }

    IEnumerator Settings()
    {
        HorizontalTransition.SetBool("LeftAnimation", true);
        yield return new WaitForSeconds(0.3f);
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
        StartCoroutine(Menu());
    }


    IEnumerator Menu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        yield return new WaitForSeconds(0.8f);
    }


    //CONTROLS
    public void ControlsButton()
    {
        StartCoroutine(Controls());
    }

    IEnumerator Controls()
    {
        yield return new WaitForSeconds(0.8f);
    }


    public void BackControlsButton()
    {
        StartCoroutine(Settings2());
    }


    IEnumerator Settings2()
    {
        yield return new WaitForSeconds(0.8f);
    }

}
