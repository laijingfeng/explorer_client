using UnityEngine;
using System.Collections;

/// <summary>
/// 副本
/// </summary>
public class CopyUI : SingletonWindow<CopyUI>
{
    /// <summary>
    /// 离开
    /// </summary>
    private GameObject m_goButtonLeave;

    /// <summary>
    /// 重开
    /// </summary>
    private GameObject m_goButtonRestart;

    void Start()
    {
        m_goButtonLeave = Util.FindGo(gameObject, "ButtonLeave");
        UIEventListener.Get(m_goButtonLeave).onClick += OnClickButtonLeave;
        m_goButtonRestart = Util.FindGo(gameObject, "ButtonRestart");
        UIEventListener.Get(m_goButtonRestart).onClick += OnClickButtonRestart;
    }

    /// <summary>
    /// 点击离开
    /// </summary>
    /// <param name="go"></param>
    private void OnClickButtonLeave(GameObject go)
    {
        GameStateManager.Instance.ChangeState(MainSceneState.Instance);
    }

    /// <summary>
    /// 点击重开
    /// </summary>
    /// <param name="go"></param>
    private void OnClickButtonRestart(GameObject go)
    {
        GameStateManager.Instance.ChangeState(CopyState.Instance);
    }

    void Update()
    {

    }
}
