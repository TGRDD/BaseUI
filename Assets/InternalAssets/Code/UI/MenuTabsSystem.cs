using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class MenuTabsSystem : MonoBehaviour
{
    public static event Action OnNewTabOpenProccesStarted;
    public static event Action OnNewTabOpenProccesEnded;

    public event Action OnHideTabs;

    [SerializeField] private MenuTab[] _tabsArray;
    public MenuTab[] TabsArray => _tabsArray;

    [Header("Config")]
    [SerializeField] private bool _forceFirstTab = false;
    [SerializeField] private float _transitionDuration = 2f;
    [SerializeField] private Ease _transitionEase = Ease.InOutBounce;
    [SerializeField] private float _transitionOffsetY = -10;

    private MenuTab _currentTab;
    private bool _firstLoading = true;

    public string FirstTabName => _tabsArray[0].name;
    public MenuTab FirstTab => GetTabByName(FirstTabName);


    private void OnEnable()
    {
        foreach (var tab in _tabsArray)
        {
            OnHideTabs += tab.Hide;
        }
    }

    private void OnDisable()
    {
        foreach (var tab in _tabsArray)
        {
            if (tab == null) continue;
            OnHideTabs -= tab.Hide;
        }
    }

    private void Start()
    {
        HideAllTabs();

        _currentTab = FirstTab;

        if (_forceFirstTab)
        {
            ForceOpenTab(FirstTabName);
        }
        else
        {
            FirstTab.Hide();
            OpenTab(FirstTabName);
        }
    }

    public void ForceOpenTab(string TabName)
    {
        OnNewTabOpenProccesStarted?.Invoke();

        _firstLoading = false;
        SwitchTabs(TabName, out MenuTab prevTab, out MenuTab newTab);
        HideAllTabs();
        newTab.Show();
    }

    public void OpenTab(string TabName)
    {
        SwitchTabs(TabName, out MenuTab prevTab, out MenuTab newTab);

        float chachedY = prevTab.transform.position.y;

        Tween MovePreviousDown = prevTab.transform.DOMoveY(_transitionOffsetY, _transitionDuration);
        MovePreviousDown.SetEase(_transitionEase);


        TweenCallback IntermediateCallback = MovePreviousDown.onComplete = () =>
        {
            HideAllTabs();

            newTab.transform.DOMoveY(0, 0);
            newTab.Show();

            newTab.CanvasGroup.alpha = 0;
            prevTab.CanvasGroup.alpha = 1;

        };

        Tween MoveNextDown = newTab.transform.DOMoveY(chachedY, _transitionDuration);
        MoveNextDown.SetEase(_transitionEase);

        Sequence sequence = DOTween.Sequence();

        if (!_firstLoading)
        {
            sequence
                .Append(newTab.transform.DOMoveY(-_transitionOffsetY, 0))
                .Append(MovePreviousDown)
                .Join(prevTab.CanvasGroup.DOFade(0, _transitionDuration / 2))
                .AppendCallback(IntermediateCallback);
        }
        else
        {
            newTab.CanvasGroup.alpha = 0;
            newTab.Show();
            sequence.Append(newTab.transform.DOMoveY(-_transitionOffsetY, 0));


            _firstLoading = false;
        }


        sequence
            .Append(MoveNextDown)
            .Join(newTab.CanvasGroup.DOFade(1, _transitionDuration * 1.25f));

        sequence.Play();
        sequence.onComplete = () => { newTab.Execute(); OnNewTabOpenProccesEnded?.Invoke(); };
    }



    private void HideAllTabs()
    {
        OnHideTabs?.Invoke();
    }

    private MenuTab GetTabByName(string TabName)
    {
        MenuTab tmp = _tabsArray.FirstOrDefault(Tab => Tab.TabName == TabName);
        if (tmp == null) throw new NullReferenceException($"ERROR: The system does not contain a tab with the name {TabName} at {this.gameObject.name}");
        return tmp;
    }

    private void SetCurrentTab(MenuTab tab)
    {
        _currentTab = tab;
    }

    private void SwitchTabs(string TabName, out MenuTab previousTab, out MenuTab newTab)
    {
        newTab = GetTabByName(TabName);

        previousTab = _currentTab;
        _currentTab = newTab;
        SetCurrentTab(_currentTab);
    }
}
