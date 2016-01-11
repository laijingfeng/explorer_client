using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏更新状态
/// </summary>
public class GameUpdateState : GameState<GameUpdateState>
{
    public override void EnterAndLoadStaticRes()
    {
        //检测并进行更新
        OnStaticResLoaded(null);
    }

    public override void LoadDynamicRes()
    {
        base.LoadDynamicRes();
        TableLoader.Instance.LoadTables();
    }

    public override void Init()
    {
        base.Init();

        GameStateManager.Instance.ChangeState(MainSceneState.Instance);
    }

    public override void OnExit()
    {
        EntryManager.Instance.Clear();
    }
}
