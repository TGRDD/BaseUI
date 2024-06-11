using UnityEngine;

public class SettingsMonoProvider : MonoBehaviour
{
    public static event System.Action<SaveData> OnSaveDataUpdate;

    private static SaveData saveData;
    public static SaveData SaveData => saveData;

    private SaveDataFormatter saveDataFormatter = new SaveDataFormatter();


    public static bool SoundsEnabled => SaveData.SoundsVolume == 0;
    public static bool MusicEnabled => SaveData.MusicVolume == 0;

    private void Start()
    {
        if (saveData == null)
        {
            saveData = saveDataFormatter.LoadData();
            OnSaveDataUpdate?.Invoke(saveData);
        }       
    }


    public void ChangeSoundsVolume(float Value)
    {
        saveData.SoundsVolume = Value;
        saveDataFormatter.SaveData(saveData);
        OnSaveDataUpdate?.Invoke(saveData);
    }

    public void ChangeMusicVolume(float Value)
    {
        saveData.MusicVolume = Value;
        saveDataFormatter.SaveData(saveData);
        OnSaveDataUpdate?.Invoke(saveData);
    }
}
