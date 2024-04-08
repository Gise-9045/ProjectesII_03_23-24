using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject horizontal;
    [SerializeField] private GameObject vertical;

    [SerializeField] private Button startButtonSelect;

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
            startButtonSelect.Select();

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            oldPauseButton = false;

        }
        else if (pauseMenu.activeSelf && controller.GetPause() && oldPauseButton)
        {
            pauseMenu.SetActive(false);
            levelSelector.SetActive(false);
            Time.timeScale = 1.0f;
            oldPauseButton = false;
        }
        else if(!controller.GetPause())
        {
            oldPauseButton = true;
        }

    }

    #region Continue & Exit Game

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

    #endregion

    #region button manager
    
    public void ShowScene(GameObject sceneToChange)
    {
        sceneToChange.SetActive(true);
    }

    public void HideScene(GameObject sceneToHide)
    {
        sceneToHide.SetActive(false);
    }

    public void OpenSceneSelected(string  sceneNumber)
    {
        string sceneManager = "Level " + sceneNumber; 
        SceneManager.LoadScene(sceneManager); 
    }

    #endregion
    
}
