using UnityEngine;
using System.Collections;

/// <summary>
/// 副本
/// </summary>
public class CopyState : GameState<CopyState>
{
    /// <summary>
    /// 分数
    /// </summary>
    private int m_iScore = 0;

    /// <summary>
    /// 积分
    /// </summary>
    public int Score
    {
        get
        {
            return m_iScore;
        }
    }

    public override void EnterAndLoadStaticRes()
    {
        m_iScore = 0;

        Resource res = SceneManager.Instance.LoadStaticRes();
        res.onLoaded += OnStaticResLoaded;
        res.onError += OnStaticResLoaded;
    }

    public override void LoadDynamicRes()
    {
        base.LoadDynamicRes();

        Table.SCENE sceneTable = SceneManager.CurrentSceneTable;
        Resource res = ResourceManager.Instance.LoadResource(string.Format("{0}.unity3d", sceneTable.hero_name), false);
        res.onLoaded += OnHeroLoaded;
    }

    /// <summary>
    /// 英雄加载成功
    /// </summary>
    /// <param name="res"></param>
    private void OnHeroLoaded(Resource res)
    {
        GameObject go = EntryManager.Instance.CreateHero(res.MainAsset);
        go.transform.position = Level.Instance.m_PlayerPos;
    }

    /// <summary>
    /// 主角属性变化
    /// </summary>
    private void OnPlayerAttrChange()
    {
        CopyUI.Instance.RefreshAttr();

        if (PlayerAttr.Instance.Blood <= 0)
        {
            Finish(false);
        }
    }

    /// <summary>
    /// 加分
    /// </summary>
    public void AddScore()
    {
        m_iScore++;
        OnPlayerAttrChange();
    }

    /// <summary>
    /// 通过
    /// </summary>
    /// <param name="win"></param>
    public void Finish(bool win)
    {
        //触发器在OnDestroy的时候还会触发，标记结束，忽略后续事件
        Level.Instance.onTriggerFinish -= OnTriggerFinish;

        if (win)
        {
            MessageBox.Instance.Show("胜利，得分：" + m_iScore);
        }
        else
        {
            MessageBox.Instance.Show("失败");
        }

        GameStateManager.Instance.ChangeState(MainSceneState.Instance);
    }

    public override void Init()
    {
        base.Init();
        Level.Instance.Init();
        Level.Instance.onTriggerFinish -= OnTriggerFinish;
        Level.Instance.onTriggerFinish += OnTriggerFinish;
        CopyUI.Instance.Show();
        Table.SCENE sceneTable = SceneManager.CurrentSceneTable;
        PlayerAttr.Instance.Init(sceneTable.blood, sceneTable.jump_count, OnPlayerAttrChange);
    }

    /// <summary>
    /// 触发器死亡
    /// </summary>
    /// <param name="trigger"></param>
    private void OnTriggerFinish(TriggerBase trigger)
    {
        if (trigger is TriggerBoss)
        {
            AddScore();//积分方式有问题，不一定是主角杀死的
        }
        else if (trigger is TriggerRange
            && string.IsNullOrEmpty((trigger as TriggerRange).m_strItemName) == false)
        {
            AddScore();
        }

        if (trigger.m_bIsPassTrigger)
        {
            Finish(true);
        }
    }

    public override void OnExit()
    {
        CopyUI.Instance.Hide();
        EntryManager.Instance.Clear();
        SceneManager.Instance.Clear();
    }
}
