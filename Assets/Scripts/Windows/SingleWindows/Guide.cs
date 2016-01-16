using UnityEngine;
using System.Collections;

/// <summary>
/// 引导
/// </summary>
public class Guide : SingletonWindow<Guide>
{
    #region UI变量



    #endregion

    /// <summary>
    /// 提示内容位置
    /// </summary>
    public enum ContentPos
    {
        /// <summary>
        /// 上
        /// </summary>
        Top = 0,

        /// <summary>
        /// 下
        /// </summary>
        Bottom,

        /// <summary>
        /// 左
        /// </summary>
        Left,

        /// <summary>
        /// 右
        /// </summary>
        Right,
    }

    /// <summary>
    /// 引导信息
    /// </summary>
    public class GuideInfo
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="win"></param>
        /// <param name="targetName"></param>
        /// <param name="content"></param>
        /// <param name="contentPos"></param>
        /// <param name="callBack"></param>
        /// <param name="checkCollider"></param>
        /// <param name="allowFail"></param>
        /// <param name="id"></param>
        public GuideInfo(LikeWindow win, string targetName, string content = "", ContentPos contentPos = ContentPos.Left, CallBackFunc callBack = null, bool checkCollider = false, bool allowFail = false, int id = 0)
        {
            m_Win = win;
            m_WinComp = win as Component;
            m_strTargetName = targetName;
            m_strContent = content;
            m_ContentPos = contentPos;
            m_CallBackFunc -= callBack;
            m_CallBackFunc += callBack;
            m_bCheckCollider = checkCollider;
            m_bAllowFail = allowFail;
            m_iID = id;
        }

        /// <summary>
        /// 窗体
        /// </summary>
        public LikeWindow m_Win;

        /// <summary>
        /// 窗体脚本
        /// </summary>
        public Component m_WinComp;

        /// <summary>
        /// <para>目标名字</para>
        /// <para>"|"分割可以设置多个目标，随机引导一个</para>
        /// </summary>
        public string m_strTargetName;

        /// <summary>
        /// 内容
        /// </summary>
        public string m_strContent;

        /// <summary>
        /// 内容位置
        /// </summary>
        public ContentPos m_ContentPos;

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="info"></param>
        public delegate void CallBackFunc(GuideInfo info);

        /// <summary>
        /// 回调函数
        /// </summary>
        public event CallBackFunc m_CallBackFunc;

        /// <summary>
        /// 范围框是否和Collider一致
        /// </summary>
        public bool m_bCheckCollider;

        /// <summary>
        /// 是否允许失败
        /// </summary>
        public bool m_bAllowFail;

        /// <summary>
        /// ID
        /// </summary>
        public int m_iID;
    }

    /// <summary>
    /// 当前引导信息
    /// </summary>
    private GuideInfo m_CurInfo;

    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="info"></param>
    public void Show(GuideInfo info)
    {
        if (info == null)
        {
            return;
        }

        m_CurInfo = info;

        StopCoroutine("DelayShow");
        StartCoroutine("DelayShow");
    }

    /// <summary>
    /// 延时显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayShow()
    {
        yield return new WaitForEndOfFrame();
        RealShow();
        yield return null;
    }

    /// <summary>
    /// 真正显示
    /// </summary>
    private void RealShow()
    {
        if(m_CurInfo == null
            || m_CurInfo.m_Win.IsVisible == false)
        {
            return;
        }


    }
}
