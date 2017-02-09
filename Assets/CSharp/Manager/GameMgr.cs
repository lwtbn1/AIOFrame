using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameMgr{
    private GameMgr() { }
    private static GameMgr _instance;
    public static GameMgr Instance{
        get{
            if(_instance == null)
                _instance = new GameMgr();
            return _instance;
        }
    }


    GameObject gameManagerObj;
    GameObject GameManagerObj
    {
        get
        {
            if (gameManagerObj == null)
                gameManagerObj = GameObject.Find("GameManager");
            return gameManagerObj;
        }
    }

    Dictionary<string, object> m_Managers = new Dictionary<string, object>();
    public T AddManager<T>(string typeName) where T : Component
    {
        object result = null;
        m_Managers.TryGetValue(typeName, out result);
        if (result != null)
        {
            return (T)result;
        }
        Component c = GameManagerObj.AddComponent<T>();
        m_Managers.Add(typeName, c);
        return (T)c;
    }

    /// <summary>
    /// 获取系统管理器
    /// </summary>
    public T GetManager<T>(string typeName) where T : class
    {
        if (!m_Managers.ContainsKey(typeName))
        {
            return default(T);
        }
        object manager = null;
        m_Managers.TryGetValue(typeName, out manager);
        return (T)manager;
    }
    
}
