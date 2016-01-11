using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏状态管理器
/// </summary>
public class GameStateManager : SingletonMono<GameStateManager>
{
    /// <summary>
    /// 上一个状态
    /// </summary>
    private IGameState lastState = null;
    
    /// <summary>
    /// 当前状态
    /// </summary>
    private IGameState curState = null;
    
    /// <summary>
    /// 下一个状态
    /// </summary>
    private IGameState nextState = null;

    /// <summary>
    /// 当前状态
    /// </summary>
    public IGameState CurState
    {
        get
        {
            return curState;
        }
    }

    /// <summary>
    /// 是否正在加载
    /// </summary>
    private bool isLoading = false;

    /// <summary>
    /// 是否正在加载
    /// </summary>
    public bool IsLoading
    {
        get
        {
            return isLoading;
        }

        set
        {
            if (isLoading == value)
            {
                return;
            }

            isLoading = value;

            if (isLoading)
            {
                StartCoroutine("WaitForFrameEnd");
            }
        }
    }

    /// <summary>
    /// 等候一帧清理垃圾切换状态
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForFrameEnd()
    {
        yield return new WaitForEndOfFrame();

        if (curState != null)
        {
            curState.OnExit();

            ResourceManager.Instance.Clear();
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        lastState = curState;
        curState = nextState;

        curState.EnterAndLoadStaticRes();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(IGameState state)
    {
        if (IsLoading)
        {
            return;
        }

        nextState = state;

        IsLoading = true;
    }   
}
