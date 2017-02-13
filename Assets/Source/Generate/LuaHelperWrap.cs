﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaHelperWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaHelper), typeof(System.Object));
		L.RegFunction("GetResManager", GetResManager);
		L.RegFunction("GetUIManager", GetUIManager);
		L.RegFunction("AddUpdateEvent", AddUpdateEvent);
		L.RegFunction("RemoveUpdateEvent", RemoveUpdateEvent);
		L.RegFunction("New", _CreateLuaHelper);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaHelper(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				LuaHelper obj = new LuaHelper();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: LuaHelper.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			ResManager o = LuaHelper.GetResManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUIManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			UIManager o = LuaHelper.GetUIManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddUpdateEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 1);
			LuaTable arg1 = ToLua.CheckLuaTable(L, 2);
			LuaHelper.AddUpdateEvent(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveUpdateEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 1);
			LuaTable arg1 = ToLua.CheckLuaTable(L, 2);
			LuaHelper.RemoveUpdateEvent(arg0, arg1);
			return 0;
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

