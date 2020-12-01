using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScreen : MonoBehaviour
{
    public void ResetSave()
    {
        FindObjectOfType<GameSession>().ResetSaveGame();
        StartCoroutine(LoadStart());
    }

    public void Return()
    {
        StartCoroutine(LoadStart());
    }

    IEnumerator LoadStart()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Start");
    }


}
