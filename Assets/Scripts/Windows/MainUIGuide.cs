using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 主界面引导
/// </summary>
public class MainUIGuide : Singleton<MainUIGuide>
{
    /// <summary>
    /// 引导内容
    /// </summary>
    private static List<Guide.GuideInfo> m_listInfo = new List<Guide.GuideInfo>()
    {
        new Guide.GuideInfo(MainUI.Instance,"Btn1","点击按钮1", Guide.ContentPos.Right, OnCallBack),
        new Guide.GuideInfo(MainUI.Instance,"Btn2","点击按钮2", Guide.ContentPos.Left, OnCallBack),
        new Guide.GuideInfo(MainUI.Instance,"Btn3","点击按钮3", Guide.ContentPos.Left, OnCallBack, false),
    };

    /// <summary>
    /// 当前索引
    /// </summary>
    private static int m_iCurIndex = -1;

    /// <summary>
    /// 当前信息
    /// </summary>
    private static Guide.GuideInfo CurInfo
    {
        get
        {
            if (m_iCurIndex < 0
                || m_iCurIndex >= m_listInfo.Count)
            {
                return null;
            }

            return m_listInfo[m_iCurIndex];
        }
    }

    /// <summary>
    /// 引导
    /// </summary>
    /// <param name="win"></param>
    public void ProcessGuide(LikeWindow win)
    {
        if (win == MainUI.Instance)
        {
            m_iCurIndex = 0;
        }

        if (CurInfo == null
            || CurInfo.m_Win != win)
        {
            return;
        }

        DoGuide();
    }

    /// <summary>
    /// 进行引导
    /// </summary>
    private static void DoGuide()
    {
        Guide.GuideInfo info = CurInfo;
        if (info == null)
        {
            return;
        }
        info.m_iID = m_iCurIndex;
        Guide.Instance.Show(info);
    }

    /// <summary>
    /// 回调
    /// </summary>
    /// <param name="info"></param>
    private static void OnCallBack(Guide.GuideInfo info)
    {
        m_iCurIndex++;
        DoGuide();
    }
}
