using UnityEngine;
using UnityEngine.Audio;

public class SoundsSingletone : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (FindObjectsOfType<AmbientMusicPlayer>().Length > 1) { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SettingsMonoProvider.OnSaveDataUpdate += ApplyData;
    }
    private void OnDisable()
    {
        SettingsMonoProvider.OnSaveDataUpdate -= ApplyData;
    }

    public void ApplyData(SaveData data)
    {
        audioMixer.SetFloat("MusicVol", data.MusicVolume);
        audioMixer.SetFloat("SoundsVol", data.SoundsVolume);
    }
}
