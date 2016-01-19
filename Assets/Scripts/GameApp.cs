using UnityEngine;
using System.Collections;

public class GameApp : SingletonMono<GameApp> 
{
    /// <summary>
    /// 是否暂停中
    /// </summary>
    private bool paused = false;

	void Awake() 
    {
        LoadSingleWindow();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

    void Start()
    {
        StopCoroutine("GameStart");
        StartCoroutine("GameStart");
    }

	void Update() 
    {
        HandlePause();

        if (Input.GetKeyDown(KeyCode.Escape)
            || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
	}

    /// <summary>
    /// 游戏开始
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        //等候一帧，让windows的Start执行完，不然可能窗体的变量未绑定
        yield return new WaitForEndOfFrame();
        GameInit();
        yield break;
    }

    /// <summary>
    /// 游戏初始化
    /// </summary>
    private void GameInit()
    {
         GameStateManager.Instance.ChangeState(GameUpdateState.Instance);
    }

    /// <summary>
    /// <para>加载窗体</para>
    /// <para>没有手动放到Hierarchy的窗体都加载到2DUICamera下</para>
    /// </summary>
    private void LoadSingleWindow()
    {
        GameObject goUICamera = Util.FindGo(gameObject, "2DUICamera");
        Object[] wins = Resources.LoadAll("SingleWindows");

        foreach (Object obj in wins)
        {
            GameObject go = GameObject.Find(obj.name);
            if (go == null)
            {
                go = Object.Instantiate(obj) as GameObject;
                go.name = go.name.Replace("(Clone)", "");
                go.transform.parent = goUICamera.transform;
            }
        }
    }

    /// <summary>
    /// 处理暂停
    /// </summary>
    private void HandlePause()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
