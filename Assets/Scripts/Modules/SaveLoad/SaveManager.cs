using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonNoMono<SaveManager>
{
    private SaveData currentSaveData;
    private ConfigSaveData currentConfigData;
    public SaveManager()
    {
        currentSaveData = LoadSaveDataByDeserialization();
        currentConfigData = LoadConfigDataByDeserialization();
        if (currentSaveData == null)
        {
            currentSaveData = new SaveData();
        }
        if (currentConfigData == null)
        {
            currentConfigData = new ConfigSaveData();
        }
    }
    
    private void SavePlayerData()
    {
        FileStream fs = File.Create(Application.persistentDataPath + "/saveData.sav");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, currentSaveData);
        fs.Close();
    }
    
    private void SaveConfigData()
    {
        FileStream fs = File.Create(Application.persistentDataPath + "/configData.sav");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, currentConfigData);
        fs.Close();
    }

    public void SetGlobalVolumeSave(float value)
    {
        currentConfigData.globalVolume = value;
        SaveConfigData();
    }
    public void SetBanBgmSave(bool value)
    {
        currentConfigData.banMusic = value;
        SaveConfigData();
    }
    public float GetGlobalVolumeSave()
    {
        return currentConfigData.globalVolume;
    }
    public bool GetBanBgmSave()
    {
        return currentConfigData.banMusic;
    }
    
    private SaveData LoadSaveDataByDeserialization()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.sav")) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/saveData.sav", FileMode.Open);
            SaveData save = bf.Deserialize(fs) as SaveData;
            fs.Close();
            return save;
        }
        Debug.LogError("Save Data Not Found");
        return null;
    }
    private ConfigSaveData LoadConfigDataByDeserialization()
    {
        if (File.Exists(Application.persistentDataPath + "/configData.sav")) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/configData.sav", FileMode.Open);
            ConfigSaveData save = bf.Deserialize(fs) as ConfigSaveData;
            fs.Close();
            return save;
        }
        Debug.LogError("Config Save Data Not Found");
        return null;
    }

    #region Only for GM
    public void GMClearSave()
    {
        currentSaveData = new SaveData();
        SavePlayerData();
    }
    public void GMPerfectSave()
    {
        currentSaveData = new SaveData();
        //var sceneConfig = Resources.Load<SO_SceneConfig>("SO_Assets/LevelConfig");
        SavePlayerData();
    }
    #endregion
}
