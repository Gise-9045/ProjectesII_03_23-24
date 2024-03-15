using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] GameObject horizontal;
    [SerializeField] GameObject vertical;

    private Animator verticalAnim;
    private Animator horizontalAnim;

    void Start()
    {
        verticalAnim = vertical.GetComponentInChildren<Animator>();
        horizontalAnim = horizontal.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;

        }
        else if (pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
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
