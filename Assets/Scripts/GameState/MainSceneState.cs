using UnityEngine;
using System.Collections;

/// <summary>
/// 主场景
/// </summary>
public class MainSceneState : GameState<MainSceneState>
{
    public override void Init()
    {
        base.Init();
        MainUI.Instance.Show();
    }

    public override void OnExit()
    {
        MainUI.Instance.Hide();
        EntryManager.Instance.Clear();
    }
}
