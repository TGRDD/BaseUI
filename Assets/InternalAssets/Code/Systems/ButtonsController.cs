using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void OnEnable()
    {
        MenuTabsSystem.OnNewTabOpenProccesStarted += TurnOffAllButtons;
        MenuTabsSystem.OnNewTabOpenProccesEnded += TurnOnAllButtons;
    }

    private void OnDisable()
    {
        MenuTabsSystem.OnNewTabOpenProccesStarted -= TurnOffAllButtons;
        MenuTabsSystem.OnNewTabOpenProccesEnded -= TurnOnAllButtons;
    }

    public void TurnOffAllButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    public void TurnOnAllButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    [ContextMenu("DebugFindButtons")]
    public void FindButtons()
    {
        buttons = FindObjectsOfType<Button>();
    }
}
