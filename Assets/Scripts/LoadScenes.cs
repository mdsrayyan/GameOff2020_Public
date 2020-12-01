using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string nextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadPreviousLevelCoroutine());
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }
    private IEnumerator LoadPreviousLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(nextLevelName);
    }
}
