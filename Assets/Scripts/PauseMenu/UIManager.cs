using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private InputActionReference pauseController;

    private void Update()
    {
        if(!pauseMenu.activeSelf && pauseController.action.WasPressedThisFrame())
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        else if(pauseMenu.activeSelf && pauseController.action.WasPressedThisFrame())
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;


        }

    }


    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
