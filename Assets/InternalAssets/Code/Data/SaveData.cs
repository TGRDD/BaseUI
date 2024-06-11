using System;

[Serializable]
public class SaveData
{
    public float SoundsVolume;
    public float MusicVolume;

    public SaveData(float soundsVolume, float musicVolume)
    {
        SoundsVolume = soundsVolume;
        MusicVolume = musicVolume;
    }

    public SaveData()
    {
        SoundsVolume = 0;
        MusicVolume = 0;
    }
}
