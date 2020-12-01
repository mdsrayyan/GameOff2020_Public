using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject levelComplete;
    [SerializeField]
    private GameObject deathAnimation;
    private GameSession gameSession;
    public LevelData currentLevelData;
    public bool collectible1 = false;
    public bool collectible2 = false;
    public bool collectible3 = false;
    private float currentTimeScale;
    public string nextLevelName;
    private float levelTimer;
    bool executeOnce = true;
    public enum Status
    {
        Started,
        Running,
        Paused,
        Death,
        Won
    }

    public Status levelStatus;
    // Start is called before the first frame update
    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        Debug.Log(gameSession.currentSave.FindLevelData(SceneManager.GetActiveScene().name));
        if (gameSession.currentSave.FindLevelData(SceneManager.GetActiveScene().name) == null)
        {
            gameSession.currentSave.AddLevelData(new LevelData(SceneManager.GetActiveScene().name));
        }
        gameSession.currentSave.currentLevel = SceneManager.GetActiveScene().name;
        SaveSystem.UpdateSave(FindObjectOfType<GameSession>().currentSave);

        currentLevelData = gameSession.currentSave.FindLevelData(SceneManager.GetActiveScene().name);
        currentLevelData.playCount++;
        collectible1 = currentLevelData.collectible1;
        collectible2 = currentLevelData.collectible2;
        collectible3 = currentLevelData.collectible3;
        Debug.Log("saved collectible1: " + collectible1);
        Debug.Log("saved collectible2: " + collectible2);
        Debug.Log("saved collectible3: " + collectible3);
    }

    private void Start()
    {
        
        levelStatus = Status.Started;
        pauseMenu.SetActive(false);
        levelComplete.SetActive(false);
        Time.timeScale = 1.0f;
        levelTimer = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
        if(levelStatus == Status.Death)
        {
            StartCoroutine(DeathProgram());
        }
        else if(levelStatus == Status.Won && executeOnce)
        {
            WonProgram();
            executeOnce = false;
        }
    }

    private void WonProgram()
    {
        FindObjectOfType<Player>().GetComponent<PlayerInputHandler>().enabled = false;
        currentLevelData.collectible1 = collectible1;
        currentLevelData.collectible2 = collectible2;
        currentLevelData.collectible3 = collectible3;
        Debug.Log("collectible1: " + collectible1);
        Debug.Log("collectible2: " + collectible2);
        Debug.Log("collectible3: " + collectible3);


        if (levelTimer < currentLevelData.bestTime)
        {
            currentLevelData.bestTime = levelTimer;
        }
        WriteData();
        StartCoroutine(LoadLevelComplete());

        //StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadLevelComplete()
    {
        yield return new WaitForSeconds(1.5f);
        levelComplete.SetActive(true);
        levelComplete.GetComponent<LevelComplete>().UpdateBestTime(currentLevelData.bestTime);
        levelComplete.GetComponent<LevelComplete>().UpdateCurrentTime(levelTimer-1.5f);
        levelComplete.GetComponent<LevelComplete>().UpdateCollectibles(currentLevelData.GetCollectibles());
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }
    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(nextLevelName);
    }

    void WriteData()
    {
        CreateNextLevelData();
        currentLevelData.completed = true;
        SaveSystem.UpdateSave(FindObjectOfType<GameSession>().currentSave);
    }

    private void CreateNextLevelData()
    {
        FindObjectOfType<GameSession>().currentSave.AddLevelData(new LevelData(nextLevelName));
    }

    IEnumerator DeathProgram()
    {
        GameObject player = GameObject.Find("Player");
        Vector2 pos = player.GetComponent<Transform>().position;
        player.SetActive(false);
        GameObject animation = Instantiate(deathAnimation, null, true);
        animation.transform.position = pos;
        DontDestroyOnLoad(animation);
        levelStatus = Status.Running;
        Debug.Log("played Death");
        yield return new WaitForSecondsRealtime(1.0f);
        currentLevelData.deaths++;
        SaveSystem.UpdateSave(FindObjectOfType<GameSession>().currentSave);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnableDisablePause()
    {
        if(pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            levelStatus = Status.Running;
        }

        else
        {
            levelStatus = Status.Paused;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
