using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientMusicPlayer : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectsOfType<AmbientMusicPlayer>().Length > 1) { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }
}
