using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField, HideInInspector] protected Image _image;
    [SerializeField, HideInInspector] protected Button _button;
    protected virtual bool State { get; set; }

    public void OnValidate()
    {
        _image ??= GetComponent<Image>();
        _button ??= GetComponent<Button>();
    }

    public virtual void Start()
    {
        UpdateView();
        _button.onClick.AddListener(SwitchState);
    }

    public virtual void SwitchState()
    {
        State = !State;
        UpdateView();
    }

    public void UpdateView()
    {
        _image.color = State ? Color.green : Color.red;
    }
}
