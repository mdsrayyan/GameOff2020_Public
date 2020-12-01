using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{
    public string world1;
    public string world2;
    public string world3;
    public string prevScene;

    public GameObject[] worldUI;

    private int selectedIndex;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = 0;
    }

    public void LoadWorld1()
    {
        StartCoroutine(LoadSceneCoroutine(world1));
    }

    public void LoadWorld2()
    {
        StartCoroutine(LoadSceneCoroutine(world2));
    }

    public void LoadWorld3()
    {
        StartCoroutine(LoadSceneCoroutine(world3));
    }

    public void LoadPrevScene()
    {
        StartCoroutine(LoadSceneCoroutine(prevScene));
    }

    public void HighlightNext()
    {
        DisableAll();
        if(selectedIndex < worldUI.Length-1)
        {
            selectedIndex++;
        }
        worldUI[selectedIndex].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(worldUI[selectedIndex]);
    }

    public void HighlightPrevious()
    {
        DisableAll();
        if (selectedIndex > 0)
        {
            selectedIndex--;
        }
        worldUI[selectedIndex].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(worldUI[selectedIndex]);

    }


    public void DisableAll()
    {
        for (int i = 0; i < worldUI.Length; i++)
        {
            worldUI[i].SetActive(false);
        }
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(sceneName);
    }


}
