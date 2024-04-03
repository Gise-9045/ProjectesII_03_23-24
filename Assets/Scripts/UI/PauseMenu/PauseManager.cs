using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelSelectorScreen; 
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    private InputController controller;


    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();

        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
    }

    private void Update()
    {
        if (!pauseMenu.activeSelf && controller.GetPause())
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;

        }
        else if (pauseMenu.activeSelf && controller.GetPause())
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
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
