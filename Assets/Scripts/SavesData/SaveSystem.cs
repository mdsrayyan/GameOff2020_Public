using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void CreateSave(string saveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+saveName+".sav";

        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData saveData = new SaveData(saveName);

        formatter.Serialize(stream, saveData);
        stream.Close();

    }


    public static void UpdateSave(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + saveData.playerName + ".sav";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();

    }


    public static SaveData LoadData(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".sav";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
            

        } else
        {
            Debug.Log("File Not Found");
            return null;
        }
    }
}
