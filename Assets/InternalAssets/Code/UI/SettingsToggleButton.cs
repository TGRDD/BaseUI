using UnityEngine;
public class SettingsToggleButton : ToggleButton
{
    private SettingsMonoProvider monoProvider;

    [SerializeField] private SettingToToggle _settingToToggle;
    protected override bool State
    {
        get 
        {
            switch (_settingToToggle)
            {
                case SettingToToggle.Sounds:
                    return SettingsMonoProvider.SoundsEnabled;

                case SettingToToggle.Music:
                    return SettingsMonoProvider.MusicEnabled;

                default:
                    return false;
            }
        }
        set 
        { }
    }

    private void Awake()
    {
        monoProvider = FindObjectOfType<SettingsMonoProvider>();
    }

    public override void Start()
    {
        UpdateView();
        _button.onClick.AddListener(SwitchState);
    }


    public override void SwitchState()
    {
        switch (_settingToToggle)
        {
            case SettingToToggle.Sounds:
                float soundValue = State ? -80f : 0f;
                monoProvider.ChangeSoundsVolume(soundValue);
                break;

            case SettingToToggle.Music:
                float musicValue = State ? -80f : 0f;
                monoProvider.ChangeMusicVolume(musicValue);
                break;

            default:
                break;
        }

        UpdateView();
    }

}


public enum SettingToToggle
{
    Music,
    Sounds
}
