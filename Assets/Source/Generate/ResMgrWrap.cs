﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ResMgrWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ResMgr), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("LoadSpriteSyn", LoadSpriteSyn);
		L.RegFunction("LoadSpriteAsyn", LoadSpriteAsyn);
		L.RegFunction("LoadUIBundlesAsyn", LoadUIBundlesAsyn);
		L.RegFunction("UnLoadUIBundles", UnLoadUIBundles);
		L.RegFunction("LoadPanelSyn", LoadPanelSyn);
		L.RegFunction("LoadPanelAsyn", LoadPanelAsyn);
		L.RegFunction("getUIBundle", getUIBundle);
		L.RegFunction("UnloadUIBundle", UnloadUIBundle);
		L.RegFunction("getPanelBundle", getPanelBundle);
		L.RegFunction("UnloadPanelBundl", UnloadPanelBundl);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSpriteSyn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			UnityEngine.Sprite o = obj.LoadSpriteSyn(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSpriteAsyn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			System.Action<UnityEngine.Sprite> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (System.Action<UnityEngine.Sprite>)ToLua.CheckObject(L, 4, typeof(System.Action<UnityEngine.Sprite>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 4);
				arg2 = DelegateFactory.CreateDelegate(typeof(System.Action<UnityEngine.Sprite>), func) as System.Action<UnityEngine.Sprite>;
			}

			System.Collections.IEnumerator o = obj.LoadSpriteAsyn(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadUIBundlesAsyn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			System.Collections.Generic.List<string> arg0 = (System.Collections.Generic.List<string>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<string>));
			System.Collections.IEnumerator o = obj.LoadUIBundlesAsyn(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnLoadUIBundles(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			System.Collections.Generic.List<string> arg0 = (System.Collections.Generic.List<string>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<string>));
			obj.UnLoadUIBundles(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPanelSyn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			UnityEngine.GameObject o = obj.LoadPanelSyn(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPanelAsyn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			System.Action<UnityEngine.GameObject> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (System.Action<UnityEngine.GameObject>)ToLua.CheckObject(L, 4, typeof(System.Action<UnityEngine.GameObject>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 4);
				arg2 = DelegateFactory.CreateDelegate(typeof(System.Action<UnityEngine.GameObject>), func) as System.Action<UnityEngine.GameObject>;
			}

			obj.LoadPanelAsyn(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getUIBundle(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.AssetBundle o = obj.getUIBundle(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadUIBundle(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.UnloadUIBundle(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getPanelBundle(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.AssetBundle o = obj.getPanelBundle(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadPanelBundl(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ResMgr obj = (ResMgr)ToLua.CheckObject(L, 1, typeof(ResMgr));
			string arg0 = ToLua.CheckString(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.UnloadPanelBundl(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_ToString(IntPtr L)
	{
		object obj = ToLua.ToObject(L, 1);

		if (obj != null)
		{
			LuaDLL.lua_pushstring(L, obj.ToString());
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}

		return 1;
	}
}

