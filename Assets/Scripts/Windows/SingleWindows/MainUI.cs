using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 主界面
/// </summary>
public class MainUI : SingletonWindow<MainUI>
{
    /// <summary>
    /// 预设
    /// </summary>
    private GameObject m_goPrefab;

    /// <summary>
    /// 格子
    /// </summary>
    private UIGrid m_gdGrid;

    /// <summary>
    /// 按钮1
    /// </summary>
    private GameObject m_goBtn1;

    /// <summary>
    /// 面板
    /// </summary>
    private UIDraggablePanel m_dpDraggablePanel;

    void Start()
    {
        m_goPrefab = Util.FindGo(gameObject, "Prefab");
        m_goPrefab.SetActive(false);
        m_gdGrid = Util.FindCo<UIGrid>(gameObject, "Grid");
        m_dpDraggablePanel = Util.FindCo<UIDraggablePanel>(gameObject, "DraggablePanel");

        m_goBtn1 = Util.FindGo(gameObject, "Btn1");
        ToolTip.SetToolTip(m_goBtn1, "hello, btn1");
        UIEventListener.Get(m_goBtn1).onClick += OnClickBtn1;
    }

    public override void OnShow()
    {
        Util.DestroyAllChildrenImmediate(m_gdGrid.gameObject);
        Table.SCENE sceneTable;
        Dictionary<uint, Table.SCENE>.Enumerator e = SceneTableManager.Instance.dic.GetEnumerator();

        bool bEmpty = true;

        while (e.MoveNext())
        {
            sceneTable = e.Current.Value;
            GameObject go = NGUITools.AddChild(m_gdGrid.gameObject, m_goPrefab);
            GameObject btn = Util.FindGo(go, "Button");
            UILabel lb = Util.FindCo<UILabel>(btn, "Label");

            lb.text = sceneTable.name;
            CustomData.Set(btn, sceneTable.id);
            UIEventListener.Get(btn).onClick += OnClickButton;

            bEmpty = false;
        }

        m_gdGrid.Reposition();

        if (!bEmpty)
        {
            m_dpDraggablePanel.ResetPosition();
        }
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    /// <param name="go"></param>
    private void OnClickButton(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        uint sceneID = (uint)CustomData.Get(go);

        SceneManager.Instance.ChangeStateWithScene(sceneID);
    }

    /// <summary>
    /// 点击按钮1
    /// </summary>
    /// <param name="go"></param>
    private void OnClickBtn1(GameObject go)
    {
        MessageBox.Instance.Show("Btn1");
    }

    public override void OnHide()
    {
        if (m_gdGrid != null)
        {
            Util.DestroyAllChildrenImmediate(m_gdGrid.gameObject);
        }
    }
}
