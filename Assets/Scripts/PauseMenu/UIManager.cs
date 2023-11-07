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

        }
        else if(pauseMenu.activeSelf && pauseController.action.WasPressedThisFrame())
        {
            pauseMenu.SetActive(false);

        }

    }


    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
