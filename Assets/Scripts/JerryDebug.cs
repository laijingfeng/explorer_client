using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JerryDebug : MonoBehaviour
{
    /// <summary>
    /// 最大日志量
    /// </summary>
    private const int MAX_MSG_CNT = 100;

    /// <summary>
    /// 单例
    /// </summary>
    private static JerryDebug m_instance = null;

    /// <summary>
    /// 消息信息
    /// </summary>
    private class MsgInfo
    {
        public string m_strMessage;
        public UnityEngine.Color m_color;
    }

    /// <summary>
    /// 已有的信息列表
    /// </summary>
    private static List<MsgInfo> m_listMessageList = new List<MsgInfo>();

    /// <summary>
    /// Log面板当前浏览进度
    /// </summary>
    private Vector2 m_vtScrollView = Vector2.zero;

    /// <summary>
    /// 窗口大小
    /// </summary>
    private static Rect m_rect;

    public static void Log(string strMessage)
    {
        AddLog(strMessage, new Color(255 / 255f, 255 / 255f, 255 / 255f, 1));
    }

    public static void LogWarning(string strMessage)
    {
        AddLog(strMessage, new Color(255 / 255f, 255 / 255f, 0 / 255f, 1));
    }

    public static void LogError(string strMessage)
    {
        AddLog(strMessage, new Color(255 / 255f, 0 / 255f, 0 / 255f, 1));
    }

    private static void AddLog(string strMessage, UnityEngine.Color color)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (m_instance == null)
        {
            GameObject go = new GameObject("JerryDebug");
            m_instance = go.AddComponent<JerryDebug>();
            m_rect = new Rect(0, 0, Screen.width * 0.5f, Screen.height * 0.5f);
            DontDestroyOnLoad(go);
        }

        if (m_listMessageList.Count > MAX_MSG_CNT)
        {
            m_listMessageList.RemoveAt(0);
        }

        m_listMessageList.Add(new MsgInfo()
        {
            m_strMessage = System.DateTime.Now.ToString("HH:mm:ss") + " : " + strMessage,
            m_color = color
        });
    }

    void OnGUI()
    {
        m_rect = GUI.Window(0, m_rect, RefreshUI, "JerryDebug");
    }

    /// <summary>
    /// 刷新信息
    /// </summary>
    private void RefreshUI(int iWindowID)
    {
        float width = Screen.width * 0.5f;
        float height = Screen.height * 0.5f;
        float width1 = 15, height1 = 25;
        GUI.DragWindow(new Rect(0, 0, width - width1 - 10, height/* - height1*/));//设置了Box自动换行，不会出现下面的进度条，可以都设置为可拖动区域
        m_vtScrollView = GUILayout.BeginScrollView(m_vtScrollView, GUILayout.Width(width - width1), GUILayout.Height(height - height1));
        for (int i = 0, imax = m_listMessageList.Count; i < imax; i++)
        {
            GUI.color = m_listMessageList[i].m_color;
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            GUI.skin.box.wordWrap = true;
            GUILayout.Box(m_listMessageList[i].m_strMessage);
        }
        GUILayout.EndScrollView();
    }
}
