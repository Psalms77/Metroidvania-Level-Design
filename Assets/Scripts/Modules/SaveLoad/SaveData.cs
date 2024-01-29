using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class SaveData
{
    public bool hasClear;
    public List<string> clearedSceneName = new List<string>();
    public List<string> unlockedSceneName = new List<string>();
    public int clearedMaxChapter;
    public int unlockedMaxChapter;

    public SaveData()
    {
        unlockedMaxChapter = 0;
        //unlockedSceneName.Add(LevelManager.instance.GetLevelName(0, 0));
    }
}


[System.Serializable] 
public class ConfigSaveData
{
    public bool banMusic;
    public float globalVolume = 1f;
}