using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string nextScene;
    public string statsScene;
    public string settingsScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextScene()
    {
        FindObjectOfType<GameSession>().LoadSaveGame();
        StartCoroutine(LoadSceneCoroutine(nextScene));
    }

    public void Resume()
    {
        FindObjectOfType<GameSession>().LoadSaveGame();
        string currentLevel = FindObjectOfType<GameSession>().currentSave.currentLevel;
        if(currentLevel != null)
            StartCoroutine(LoadSceneCoroutine(FindObjectOfType<GameSession>().currentSave.currentLevel));
        else
            StartCoroutine(LoadSceneCoroutine(nextScene));
    }

    public void LoadStatsScene()
    {
        StartCoroutine(LoadSceneCoroutine(statsScene));
    }

    public void LoadSettingsScene()
    {
        StartCoroutine(LoadSceneCoroutine(settingsScene));
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
        Application.Quit();
    }
 }
