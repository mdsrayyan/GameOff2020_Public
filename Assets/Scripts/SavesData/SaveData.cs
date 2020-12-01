using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public LevelData[] levels = new LevelData[100];
    private const int maxLevels = 100;
    public int[] levelCompletedStatus = new int[20];
    public string currentWorld1Pos = "";
    private int currentIndex=0;
    public string currentLevel;


    public SaveData(string playerName)
    {
        this.playerName = playerName;
        for (int i = 0; i < maxLevels; i++)
        {
            levels[i] = new LevelData("Default");
        }
        
        for (int i = 0; i < 20; i++)
        {
            levelCompletedStatus[i] = 0;
        }

        AddLevelData(new LevelData("1-01"));
        levels[0].unlocked = true;
    }

    public void AddLevelData(LevelData levelData)
    {
        bool found = false;
        for (int i = 0; i < levels.Length; i++)
        {
            if(levels[i].levelName == levelData.levelName)
            {
                //levels[i] = levelData;
                found = true;
            }
        }

        if(!found)
        {
            levels[currentIndex] = levelData;
            currentIndex++;
        }
    }

    public LevelData FindLevelData(string levelName)
    {
        foreach (LevelData data in levels)
        {
            
            if(data.levelName == levelName)
            {
                return data;
            }
        }

        return null;
    }
}
