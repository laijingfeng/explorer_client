using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏状态接口
/// </summary>
public interface IGameState
{
    /// <summary>
    /// 进入并加载静态资源
    /// </summary>
    void EnterAndLoadStaticRes();

    /// <summary>
    /// 加载动态资源
    /// </summary>
    void LoadDynamicRes();

    /// <summary>
    /// 初始化
    /// </summary>
    void Init();

    /// <summary>
    /// 退出
    /// </summary>
    void OnExit();
}

/// <summary>
/// 游戏状态
/// </summary>
public class GameState<T> : SingletonMono<T>, IGameState where T : UnityEngine.Component
{
    /// <summary>
    /// 进入并加载静态资源
    /// </summary>
    public virtual void EnterAndLoadStaticRes() 
    { 
        OnStaticResLoaded(null); 
    }

    /// <summary>
    /// 加载动态资源
    /// </summary>
    public virtual void LoadDynamicRes() { }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init() { }

    /// <summary>
    /// 退出
    /// </summary>
    public virtual void OnExit() { }

    /// <summary>
    /// 静态资源加载成功
    /// </summary>
    /// <param name="res"></param>
    protected void OnStaticResLoaded(Resource res)
    {
        LoadDynamicRes();

        Resource r = ResourceManager.Instance.LoadResource("Config.unity3d");
        r.onLoaded += OnDynamicResLoaded;
        r.onError += OnDynamicResLoaded;
    }

    /// <summary>
    /// 静态资源加载成功
    /// </summary>
    /// <param name="res"></param>
    protected void OnDynamicResLoaded(Resource res)
    {
        //置为加载成功放在Init前，Init表示进入了当前场景，要可以进行场景切换操作
        GameStateManager.Instance.IsLoading = false;
        Init();
    }
}
