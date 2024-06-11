using UnityEngine;

public class MenuTab : MonoBehaviour
{
    [SerializeField, HideInInspector] private CanvasGroup _canvasGroup;
    public string TabName => gameObject.name;
    public CanvasGroup CanvasGroup => _canvasGroup;

    private void OnValidate()
    {
        _canvasGroup ??= GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
