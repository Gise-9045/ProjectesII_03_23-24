using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject exitDialog;

    [SerializeField] private Animator[] mainMenuAnim;
    [SerializeField] private Animator[] optionsAnim;

    //MENÚ DE INICIO
    private void Start()
    {
        mainMenu.SetActive(true);

        for (int i = 0; i < mainMenuAnim.LongLength; i++)
        {
            mainMenuAnim[i].SetBool("isActive", true);
        }
    }


    public void PlayButton()
    {
        SceneManager.LoadScene("Gym");
    }

    public void SettingsButton()
    {
        StartCoroutine(Settings());
    }

    IEnumerator Settings()
    {
        for (int i = 0; i < mainMenuAnim.LongLength; i++)
        {
            mainMenuAnim[i].SetBool("isActive", false);
        }

        yield return new WaitForSeconds(1);
        mainMenu.SetActive(false);

        yield return new WaitUntil(() => !mainMenu.activeSelf);

        options.SetActive(true);


        for (int i = 0; i < optionsAnim.LongLength; i++)
        {
            optionsAnim[i].SetBool("isActive", true);
        }

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
        for (int i = 0; i < optionsAnim.LongLength; i++)
        {
            optionsAnim[i].SetBool("isActive", false);
        }

        yield return new WaitForSeconds(1);
        options.SetActive(false);

        yield return new WaitUntil(() => !options.activeSelf);


        mainMenu.SetActive(true);


        for (int i = 0; i < mainMenuAnim.LongLength; i++)
        {
            mainMenuAnim[i].SetBool("isActive", true);
        }

    }
}
