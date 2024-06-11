#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class TabsDevWindow : EditorWindow
{
    [MenuItem("ProjectTools/TabsDevWindow")]
    public static void ShowWindow()
    {
        GetWindow<TabsDevWindow>().Show();
    }

    private void OnGUI()
    {
        MenuTabsSystem[] systems = FindObjectsOfType<MenuTabsSystem>();

        if (systems == null) return;

        foreach (var system in systems)
        {
            if (system == null) continue;

            GUILayout.Label($"SYSTEM {system.gameObject.name}");            
            foreach (var tab in system.TabsArray)
            {
                if (tab == null) continue;

                if (GUILayout.Button($"{tab.gameObject.name}"))
                {
                    system.ForceOpenTab(tab.name);
                }
            }
        }
    }

    
}
#endif