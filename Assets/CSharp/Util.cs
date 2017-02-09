using UnityEngine;
using System.Collections;
using LuaInterface;
public class Util {

    public static object[] CallMethod(string module, string funcName, params object[] args)
    {
        return GameMgr.Instance.GetManager<LuaMgr>("LuaMgr").CallMethod(module + "." + funcName, args);
    }
}
