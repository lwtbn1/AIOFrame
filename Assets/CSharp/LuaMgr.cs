using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LuaInterface;
class LuaMgr : LuaClient
{
    
    protected override void Awake()
    {
        base.Awake();
       
    }

    public object[] CallMethod(string funcName, params object[] args)
    {
        if (luaState != null)
        {
            LuaFunction func = luaState.GetFunction(funcName);
            if (func != null)
            {
                return func.Call(args);
            }
        }
        
        return null;
    }
}

