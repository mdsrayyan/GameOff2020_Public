using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : ICloneable
{
    public string levelName;
    public bool unlocked = true;
    public bool completed = false;
    public float bestTime = Mathf.Infinity;
    public bool collectible1 = false;
    public bool collectible2 = false;
    public bool collectible3 = false;
    public int playCount = 0;
    public int deaths = 0;
    public int successRuns = 0;
    private int numOfCollectibles;

    public LevelData(string levelName)
    {
        this.levelName = levelName;
    }

    public object Clone()
    {
        LevelData clonedData = new LevelData(this.levelName);
        clonedData.unlocked = this.unlocked;
        clonedData.completed = this.completed;
        clonedData.bestTime = this.bestTime;
        clonedData.collectible1 = this.collectible1;
        clonedData.collectible2 = this.collectible2;
        clonedData.collectible3 = this.collectible3;
        

        return clonedData;
    }

    public int GetCollectibles()
    {
        numOfCollectibles = 0;
        if (collectible1)
            numOfCollectibles++;
        if (collectible2)
            numOfCollectibles++;
        if (collectible3)
            numOfCollectibles++;

        return numOfCollectibles;
    }
}
