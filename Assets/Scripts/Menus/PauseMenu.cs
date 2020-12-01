using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] string mainMenuName;
    public GameObject resume;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resume);
    }
    public void ResumeGame()
    {
        FindObjectOfType<LevelManager>().EnableDisablePause();
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().name));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneCoroutine(mainMenuName));
    }
    public void ExitGame()
    {
        StartCoroutine(ExitGameCoroutine());
    }
    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ExitGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Debug.Log("Quit");
        Application.Quit();
    }
}
