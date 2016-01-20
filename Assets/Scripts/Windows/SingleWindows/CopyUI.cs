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

    /// <summary>
    /// 属性
    /// </summary>
    private GameObject m_goAttr;

    /// <summary>
    /// 血量
    /// </summary>
    private UILabel m_lbBlood;

    /// <summary>
    /// 连跳
    /// </summary>
    private UILabel m_lbJump;

    /// <summary>
    /// 积分
    /// </summary>
    private UILabel m_lbScore;

    void Start()
    {
        m_goButtonLeave = Util.FindGo(gameObject, "ButtonLeave");
        UIEventListener.Get(m_goButtonLeave).onClick += OnClickButtonLeave;
        m_goButtonRestart = Util.FindGo(gameObject, "ButtonRestart");
        UIEventListener.Get(m_goButtonRestart).onClick += OnClickButtonRestart;

        m_goAttr = Util.FindGo(gameObject, "Attr");
        m_lbBlood = Util.FindCo<UILabel>(m_goAttr, "Blood");
        m_lbJump = Util.FindCo<UILabel>(m_goAttr, "Jump");
        m_lbScore = Util.FindCo<UILabel>(m_goAttr, "Score");
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

    /// <summary>
    /// 刷新属性
    /// </summary>
    public void RefreshAttr()
    {
        Table.SCENE sceneTable = SceneManager.CurrentSceneTable;

        m_lbBlood.text = PlayerAttr.Instance.Blood.ToString();
        int blood = PlayerAttr.Instance.Blood - sceneTable.blood;
        if (blood != 0)
        {
            m_lbBlood.text += string.Format("({0}{1}[-])", blood > 0 ? "[00FF00]+" : "[FF0000]", blood);
        }

        m_lbJump.text = PlayerAttr.Instance.JumpCount.ToString();
        int jump = PlayerAttr.Instance.JumpCount - sceneTable.jump_count;
        if (jump != 0)
        {
            m_lbJump.text += string.Format("({0}{1}[-])", jump > 0 ? "[00FF00]+" : "[FF0000]", jump);
        }

        m_lbScore.text = CopyState.Instance.Score.ToString();
    }
}
