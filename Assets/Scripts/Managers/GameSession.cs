using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public SaveData currentSave;
    [SerializeField] private bool activateTempSave = false;
    // Start is called before the first frame update
    void Awake()
    {
        SetupSingleton();
        if(activateTempSave)
        {
            LoadSaveGame();
        }
    }

    private void SetupSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadSaveGame()
    {
        if(activateTempSave)
        {
            SaveSystem.CreateSave("tmpsave");
            currentSave = SaveSystem.LoadData("tmpsave");
        }

        else
        {
            if (SaveSystem.LoadData("DefaultPlayer") == null)
            {
                SaveSystem.CreateSave("DefaultPlayer");
            }

            currentSave = SaveSystem.LoadData("DefaultPlayer");
        }
    }

    public void ResetSaveGame()
    {
        SaveSystem.CreateSave("DefaultPlayer");
    }

}