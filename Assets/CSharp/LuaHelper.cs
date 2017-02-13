using UnityEngine;
using System.Collections;
using LuaInterface;
public class LuaHelper {

    public static ResManager GetResManager()
    {
        return GameManager.Instance.GetManager<ResManager>("ResManager");
    }

    public static UIManager GetUIManager()
    {
        return GameManager.Instance.GetManager<UIManager>("UIManager");
    }

    public static void AddUpdateEvent(LuaFunction func, LuaTable table)
    {
        GameManager.Instance.GetManager<LuaManager>("LuaManager").GetLooper().UpdateEvent.Add(func, table);
    }

    public static void RemoveUpdateEvent(LuaFunction func, LuaTable table)
    {
        GameManager.Instance.GetManager<LuaManager>("LuaManager").GetLooper().UpdateEvent.Remove(func, table);
    }
}
