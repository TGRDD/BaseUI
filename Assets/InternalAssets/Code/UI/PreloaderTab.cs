using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PreloaderTab : MenuTab
{
    public UnityEvent OnTabLoaded;

    [SerializeField] private string _sceneToLoadName;

    private void Start()
    {
        OnTabLoaded?.Invoke();        
    }

    public override void Execute()
    {
        SceneManager.LoadScene(_sceneToLoadName);
    }

    public void SetSceneToLoad(string sceneToLoadName)
    {
        _sceneToLoadName = sceneToLoadName;
    }
}
