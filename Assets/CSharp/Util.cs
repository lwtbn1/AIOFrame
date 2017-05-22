using UnityEngine;
using System.Collections;
using LuaInterface;
public class Util {

    public static object[] CallMethod(string module, string funcName, params object[] args)
    {
        return GameManager.Instance.GetManager<LuaManager>("LuaManager").CallMethod(module + "." + funcName, args);
    }

    public static object[] CallMethod(LuaTable table, string funcName, params object[] args)
    {
        LuaFunction func = null;
        if ((func = table.GetLuaFunction(funcName)) != null)
            return func.Call(args);
        return null;
    }
    
}
