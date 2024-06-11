using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public struct SaveDataFormatter
{
    private static string settingsFilePath = Application.dataPath + "/save.playersettings";

    public void SaveData(SaveData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(settingsFilePath, FileMode.Create);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public SaveData LoadData()
    {
        if (!File.Exists(settingsFilePath))
        {
            SaveData newData = new SaveData();
            SaveData(newData);
            return newData;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(settingsFilePath, FileMode.Open);

        SaveData data = (SaveData)bf.Deserialize(fs);
        fs.Close();

        return data;
    }
}
