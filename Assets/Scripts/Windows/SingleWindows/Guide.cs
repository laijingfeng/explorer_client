using UnityEngine;
using System.Collections;

/// <summary>
/// 引导
/// </summary>
public class Guide : SingletonWindow<Guide>
{
    #region UI变量

    /// <summary>
    /// 遮罩
    /// </summary>
    private GameObject m_goCover;

    /// <summary>
    /// 遮罩_右
    /// </summary>
    private UISprite m_stCoverRight;

    /// <summary>
    /// 遮罩_左
    /// </summary>
    private UISprite m_stCoverLeft;

    /// <summary>
    /// 遮罩_上
    /// </summary>
    private UISprite m_stCoverTop;

    /// <summary>
    /// 遮罩_下
    /// </summary>
    private UISprite m_stCoverBottom;

    /// <summary>
    /// 手指
    /// </summary>
    private GameObject m_goHand;

    /// <summary>
    /// 框
    /// </summary>
    private UISprite m_stKuang;

    /// <summary>
    /// 内容
    /// </summary>
    private GameObject m_goContent;

    /// <summary>
    /// 内容标签
    /// </summary>
    private UILabel m_lbContentLabel;

    /// <summary>
    /// 内容_方向
    /// </summary>
    private GameObject[] m_goContentDir = new GameObject[4];

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
        public GuideInfo()
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="win">窗体</param>
        /// <param name="targetName">目标名字</param>
        /// <param name="content">内容</param>
        /// <param name="contentPos">内容位置</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="force">是否强制引导</param>
        /// <param name="allowFail">是否允许失败</param>
        /// <param name="id">ID</param>
        public GuideInfo(LikeWindow win, string targetName, string content = "", ContentPos contentPos = ContentPos.Left, CallBackFunc callBack = null, bool force = true, bool allowFail = false, int id = 0)
        {
            m_Win = win;
            m_WinComp = win as Component;
            m_strTargetName = targetName;
            m_strContent = content;
            m_ContentPos = contentPos;
            m_CallBackFunc -= callBack;
            m_CallBackFunc += callBack;
            m_bForce = force;
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
        public CallBackFunc m_CallBackFunc;

        /// <summary>
        /// 是否强制引导
        /// </summary>
        public bool m_bForce;

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
    /// 目标
    /// </summary>
    private GameObject m_goTarget;

    /// <summary>
    /// 检测射线
    /// </summary>
    private Ray m_CheckRay;

    /// <summary>
    /// 相机
    /// </summary>
    private Camera m_UICamera;

    /// <summary>
    /// Root
    /// </summary>
    private UIRoot m_Root;

    /// <summary>
    /// 目标边框
    /// </summary>
    private Bounds m_TargetBounds;

    /// <summary>
    /// 内容框
    /// </summary>
    private Bounds m_ContentBounds;

    public override void Awake()
    {
        base.Awake();
        m_UICamera = GameObject.FindWithTag("2DUICamera").GetComponent<Camera>();
        m_Root = this.gameObject.GetComponentInChildren<UIRoot>();
    }

    void Start()
    {
        m_goCover = Util.FindGo(gameObject, "Cover");
        m_goCover.SetActive(false);
        m_stCoverRight = Util.FindCo<UISprite>(m_goCover, "Right");
        UIEventListener.Get(m_stCoverRight).onClick += OnClickCover;
        m_stCoverLeft = Util.FindCo<UISprite>(m_goCover, "Left");
        UIEventListener.Get(m_stCoverLeft).onClick += OnClickCover;
        m_stCoverTop = Util.FindCo<UISprite>(m_goCover, "Top");
        UIEventListener.Get(m_stCoverTop).onClick += OnClickCover;
        m_stCoverBottom = Util.FindCo<UISprite>(m_goCover, "Bottom");
        UIEventListener.Get(m_stCoverBottom).onClick += OnClickCover;

        m_stKuang = Util.FindCo<UISprite>(gameObject, "Kuang");
        m_stKuang.gameObject.SetActive(false);

        m_goHand = Util.FindGo(gameObject, "Hand");

        m_goContent = Util.FindGo(gameObject, "Content");
        m_ContentBounds = NGUIMath.CalculateRelativeWidgetBounds(m_Root.transform, m_goContent.transform);
        m_goContent.SetActive(false);
        m_lbContentLabel = Util.FindCo<UILabel>(m_goContent, "ContentLabel");
        for (int i = 0; i < 4; i++)
        {
            m_goContentDir[i] = Util.FindGo(m_goContent, (ContentPos.Top + i).ToString());
        }
    }

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
        if (m_CurInfo == null)
        {
            return;
        }

        if (m_CurInfo.m_Win.IsVisible == false)
        {
            PostNotice(true);
            return;
        }
        
        m_goTarget = GetTarget(m_CurInfo.m_WinComp.gameObject, m_CurInfo.m_strTargetName);

        if (m_goTarget == null)
        {
            PostNotice(true);
            return;
        }

        BoxCollider collider = m_goTarget.GetComponent<BoxCollider>();

        if (collider == null)
        {
            PostNotice(true);
            return;
        }

        Vector3 screenPos = m_goTarget.transform.position + this.gameObject.transform.TransformPoint(collider.center);
        screenPos = m_UICamera.WorldToScreenPoint(screenPos);

        m_CheckRay = m_UICamera.ScreenPointToRay(screenPos);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(m_CheckRay, out hit, m_UICamera.farClipPlane - m_UICamera.nearClipPlane, m_UICamera.cullingMask) == false)
        {
            PostNotice(true);
            return;
        }
        
        if (hit.collider.gameObject != m_goTarget)
        {
            LocalDebug(hit.collider.gameObject.transform);
            LocalDebug(m_goTarget.transform);
            PostNotice(true);
            return;
        }

        Show();
    }

    /// <summary>
    /// 调试
    /// </summary>
    /// <param name="go"></param>
    private void LocalDebug(Transform tmp)
    {
        string content = tmp.name;
        while (tmp.parent != null)
        {
            tmp = tmp.parent;
            content = tmp.name + "->" + content;
        }
        Debug.LogError(content);
    }

    public override void OnShow()
    {
        base.OnShow();

        m_goContent.SetActive(false);
        m_stKuang.gameObject.SetActive(false);

        UIEventListener.Get(m_goTarget).onClick -= OnClickTarget;
        UIEventListener.Get(m_goTarget).onClick += OnClickTarget;

        m_TargetBounds = NGUIMath.CalculateRelativeWidgetBounds(m_Root.transform, m_goTarget.transform);
        SetKuang();
        if (m_CurInfo.m_bForce)
        {
            SetCover();
        }
        else
        {
            m_goCover.SetActive(false);
        }
        SetContent();
        TweenHand();
    }

    /// <summary>
    /// 目标点击
    /// </summary>
    /// <param name="go"></param>
    private void OnClickTarget(GameObject go)
    {
        if (go != null)
        {
            UIEventListener.Get(go).onClick -= OnClickTarget;
        }

        //注意一定要先Hide再Post，因为Post里可能再次Show
        Hide();
        PostNotice();
    }

    /// <summary>
    /// 点击遮罩
    /// </summary>
    /// <param name="go"></param>
    private void OnClickCover(GameObject go)
    {
        MessageBox.Instance.Show("请按引导指引操作");
    }

    /// <summary>
    /// <para>设置遮罩</para>
    /// <para>为了防止裂缝，坐标取整</para>
    /// </summary>
    private void SetCover()
    {
        //Left
        float leftX = Mathf.RoundToInt(m_TargetBounds.center.x - m_TargetBounds.extents.x);
        m_stCoverLeft.transform.localPosition = new Vector3(leftX, 0, 0);

        //Right
        float rightX = Mathf.RoundToInt(m_TargetBounds.center.x + m_TargetBounds.extents.x);
        if ((rightX - leftX) % 2 == 1)
        {
            rightX = rightX - 1;
        }
        m_stCoverRight.transform.localPosition = new Vector3(rightX, 0, 0);

        //Top
        m_stCoverTop.transform.localPosition = new Vector3(0.5f * (rightX + leftX), m_TargetBounds.center.y + m_TargetBounds.extents.y, 0);
        m_stCoverTop.transform.localScale = new Vector3(rightX - leftX, m_stCoverTop.transform.localScale.y, 1);

        //Bottom
        m_stCoverBottom.transform.localPosition = new Vector3(0.5f * (rightX + leftX), m_TargetBounds.center.y - m_TargetBounds.extents.y, 0);
        m_stCoverBottom.transform.localScale = new Vector3(rightX - leftX, m_stCoverBottom.transform.localScale.y, 1);

        m_goCover.SetActive(true);
    }

    /// <summary>
    /// 设置框
    /// </summary>
    private void SetKuang()
    {
        m_stKuang.transform.position = new Vector3(m_goTarget.transform.position.x, m_goTarget.transform.position.y, m_stKuang.transform.position.z);
        m_stKuang.transform.localScale = new UnityEngine.Vector3(m_TargetBounds.extents.x * 2, m_TargetBounds.extents.y * 2, 1);
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    private void SetContent()
    {
        if (string.IsNullOrEmpty(m_CurInfo.m_strContent))
        {
            return;
        }

        m_lbContentLabel.text = m_CurInfo.m_strContent;

        for (int i = 0; i < 4; i++)
        {
            m_goContentDir[i].SetActive(false);
        }

        m_goContent.transform.position = new Vector3(m_goTarget.transform.position.x, m_goTarget.transform.position.y, m_goContent.transform.position.z);
        Vector3 pos = m_goContent.transform.localPosition;

        switch (m_CurInfo.m_ContentPos)
        {
            case ContentPos.Top:
                {
                    m_goContentDir[(int)ContentPos.Bottom].SetActive(true);
                    pos.y -= (m_ContentBounds.extents.y + m_TargetBounds.extents.y + 4f);//4是拉开一些距离，不要和边框重叠
                }
                break;
            case ContentPos.Bottom:
                {
                    m_goContentDir[(int)ContentPos.Top].SetActive(true);
                    pos.y += (m_ContentBounds.extents.y + m_TargetBounds.extents.y + 4f);
                }
                break;
            case ContentPos.Left:
                {
                    m_goContentDir[(int)ContentPos.Right].SetActive(true);
                    pos.x -= (m_ContentBounds.extents.x + m_TargetBounds.extents.x + 4f);
                }
                break;
            case ContentPos.Right:
                {
                    m_goContentDir[(int)ContentPos.Left].SetActive(true);
                    pos.x += (m_ContentBounds.extents.x + m_TargetBounds.extents.x + 4f);
                }
                break;
        }
        m_goContent.transform.localPosition = pos;
    }

    /// <summary>
    /// 滑动手指
    /// </summary>
    private void TweenHand()
    {
        Vector3 des = new Vector3(m_TargetBounds.center.x, m_TargetBounds.center.y, m_goHand.transform.position.z);

        TweenPosition Tweener = TweenPosition.Begin(m_goHand, 0.5f, des);
        Tweener.method = UITweener.Method.EaseInOut;

        if (Tweener != null)
        {
            Tweener.onFinished -= OnTweenFinished;
            Tweener.onFinished += OnTweenFinished;
        }
    }

    /// <summary>
    /// 滑动结束
    /// </summary>
    /// <param name="tween"></param>
    private void OnTweenFinished(UITweener tween)
    {
        tween.onFinished -= OnTweenFinished;
        m_goContent.SetActive(true);
        m_stKuang.gameObject.SetActive(true);

        StopCoroutine("CheckError");
        StartCoroutine("CheckError");
    }

    /// <summary>
    /// 定时检测引导是否被其他意外弹出的窗体遮挡，导致卡死
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckError()
    {
        while (this.IsVisible)
        {
            yield return new WaitForSeconds(2f);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(m_CheckRay, out hit, m_UICamera.farClipPlane - m_UICamera.nearClipPlane, m_UICamera.cullingMask) == false)
            {
                PostNotice(true);
                break;
            }

            if (hit.collider.gameObject != m_goTarget)
            {
                PostNotice(true);
                break;
            }
        }
        Hide();
        yield return null;
    }

    public override void OnHide()
    {
        base.OnHide();
        StopAllCoroutines();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="checkAllowFail">是否检查是否允许失败</param>
    private void PostNotice(bool checkAllowFail = false)
    {
        if (checkAllowFail
            && m_CurInfo.m_bAllowFail == false)
        {
            return;
        }

        if (m_CurInfo.m_CallBackFunc != null)
        {
            m_CurInfo.m_CallBackFunc(m_CurInfo);
        }
    }

    /// <summary>
    /// 获取目标
    /// </summary>
    /// <param name="parent">父节点</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    public GameObject GetTarget(GameObject parent, string name)
    {
        GameObject goRet = null;
        if (parent == null)
        {
            return goRet;
        }
        string child = string.Empty;
        if (name.Contains("|"))
        {
            string[] tmp = name.Split('|');
            child = tmp[Random.Range(0, tmp.Length)];
        }
        else
        {
            child = name;
        }
        goRet = Util.FindGo(parent, child, false);
        return goRet;
    }
}
