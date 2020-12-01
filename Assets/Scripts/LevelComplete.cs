using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject nextLevel;
    public GameObject currentTime;
    public GameObject collectibles;
    public GameObject bestTime;
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentTime(float timer)
    {
        currentTime.GetComponent<TextMeshProUGUI>().text = "Time Taken: " + timer.ToString();
    }

    public void UpdateBestTime(float timer)
    {
        bestTime.GetComponent<TextMeshProUGUI>().text = "Best Time: " + timer.ToString();
    }

    public void UpdateCollectibles(int num)
    {
        collectibles.GetComponent<TextMeshProUGUI>().text = "Collectibles: " + num.ToString() + "/3";
    }

    public void LoadNextLevel()
    {
        FindObjectOfType<LevelManager>().LoadNextLevel();
    }

    public void ReplaySameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
    }


}
