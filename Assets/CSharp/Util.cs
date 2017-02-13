using UnityEngine;
using System.Collections;
using LuaInterface;
public class Util {

    public static object[] CallMethod(string module, string funcName, params object[] args)
    {
        return GameManager.Instance.GetManager<LuaManager>("LuaManager").CallMethod("View/" + module + "." + funcName, args);
    }

    
}
