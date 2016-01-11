﻿using UnityEngine;
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

        Resource res = ResourceManager.Instance.LoadResource("hero.unity3d", false);
        res.onLoaded += OnHeroLoaded;
    }

    /// <summary>
    /// 加分
    /// </summary>
    public void AddScore()
    {
        m_iScore++;
    }

    /// <summary>
    /// 通过
    /// </summary>
    /// <param name="win"></param>
    public void Finish(bool win)
    {
        if (win)
        {
            m_iScore++;
            MessageBox.Instance.Show("胜利，得分：" + m_iScore);
        }
        else
        {
            MessageBox.Instance.Show("失败");
        }

        GameStateManager.Instance.ChangeState(MainSceneState.Instance);
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

    public override void Init()
    {
        base.Init();
        //Level.Instance.Init();
        CopyUI.Instance.Show();
        //Level.Instance.CreateNoFatherTriggers();
    }

    public override void OnExit()
    {
        CopyUI.Instance.Hide();
        EntryManager.Instance.Clear();
        SceneManager.Instance.Clear();
    }
}