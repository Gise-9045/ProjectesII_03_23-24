using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelSelectorScreen; 
    [SerializeField] private GameObject SettingsScene; 
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    private InputController controller;

    private bool oldPauseButton;

    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();

        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
    }

    private void Update()
    {
        if (!pauseMenu.activeSelf && controller.GetPause() && oldPauseButton)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            oldPauseButton = false;

        }
        else if (pauseMenu.activeSelf && controller.GetPause() && oldPauseButton)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            oldPauseButton = false;
        }
        else if(!controller.GetPause())
        {
            oldPauseButton = true;
        }

    }


    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameAnim());
    }

    IEnumerator ExitGameAnim()
    {
        vertical.SetActive(true);
        verticalAnim.SetBool("ExitUpAnimation", false);
        verticalAnim.SetBool("DownAnimation", true);

        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
