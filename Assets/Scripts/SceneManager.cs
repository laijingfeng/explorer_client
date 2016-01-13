using UnityEngine;
using System.Collections;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneManager : Singleton<SceneManager>
{
    /// <summary>
    /// 场景
    /// </summary>
    private GameObject sceneGo;

    /// <summary>
    /// 关卡
    /// </summary>
    private GameObject levelGo;

    /// <summary>
    /// 当前场景表
    /// </summary>
    private static Table.SCENE m_CurrentSceneTable;

    /// <summary>
    /// 上一个场景表
    /// </summary>
    private static Table.SCENE m_LastSceneTable;

    /// <summary>
    /// 加载静态资源
    /// </summary>
    /// <returns></returns>
    public Resource LoadStaticRes()
    {
        Resource res = null;
        
        if(m_CurrentSceneTable != null)
        {
            res = ResourceManager.Instance.LoadResource(string.Format("Scene/{0}.unity3d", m_CurrentSceneTable.scene_name), false);
            res.onLoaded += OnSceneLoaded;

            res = ResourceManager.Instance.LoadResource(string.Format("Scene/{0}.unity3d", m_CurrentSceneTable.level_name), false);
            res.onLoaded += OnLevelLoaded;
        }
        
        return res;
    }

    /// <summary>
    /// 关卡加载成功
    /// </summary>
    /// <param name="res"></param>
    private void OnLevelLoaded(Resource res)
    {
        if (levelGo != null)
        {
            GameObject.DestroyImmediate(levelGo);
            levelGo = null;
        }
        levelGo = UnityEngine.Object.Instantiate(res.MainAsset) as GameObject;
        levelGo.name = levelGo.name.Replace("(Clone)", "");
    }

    /// <summary>
    /// 场景加载成功
    /// </summary>
    /// <param name="res"></param>
    private void OnSceneLoaded(Resource res)
    {
        if (sceneGo != null)
        {
            GameObject.DestroyImmediate(sceneGo);
            sceneGo = null;
        }
        sceneGo = UnityEngine.Object.Instantiate(res.MainAsset) as GameObject;
        sceneGo.name = sceneGo.name.Replace("(Clone)", "");

        Scene.Instance.SetCameraRange();
    }

    /// <summary>
    /// 切换带有场景的状态
    /// </summary>
    /// <param name="sceneID"></param>
    public void ChangeStateWithScene(uint iSceneID)
    {
        m_LastSceneTable = m_CurrentSceneTable;

        if (SceneTableManager.Instance.TryGetValue(iSceneID, out m_CurrentSceneTable) == false)
        {
            return;
        }

        GameStateManager.Instance.ChangeState(CopyState.Instance);
    }

    /// <summary>
    /// 清理
    /// </summary>
    public void Clear()
    {
        if (sceneGo != null)
        {
            GameObject.Destroy(sceneGo);
            sceneGo = null;
        }

        if (levelGo != null)
        {
            GameObject.Destroy(levelGo);
            levelGo = null;
        }
    }
}
