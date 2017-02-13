﻿//**********************************************************************
// Name:        
// Author:      
// Version:     
// Date:        
// Description: 
//**********************************************************************
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
public class UIManager : MonoBehaviour
{
    string curOpenPanel;
    Dictionary<string, GameObject> panelCache = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> popupsCache = new Dictionary<string, GameObject>();
    Transform panelRoot;
    Transform PanelRoot
    {
        get{
            if (panelRoot == null)
                panelRoot = GameObject.Find("PanelRoot").transform;
            return panelRoot;
        }
    }

    Transform popupsRoot;
    Transform PopupsRoot
    {
        get
        {
            if (popupsRoot == null)
                popupsRoot = GameObject.Find("PopupsRoot").transform;
            return panelRoot;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="panelName"></param>
    /// <param name="func"></param>
    /// <param name="syn"></param>
    public void PushPanel(string bundleName, string panelName, LuaTable table, bool syn)
    {
        curOpenPanel = panelName;
        if (panelCache.ContainsKey(panelName))
        {
            GameObject willShowPanel = null;
            panelCache.TryGetValue(panelName, out willShowPanel);
            if (willShowPanel != null)
                willShowPanel.SetActive(true);
            LuaFunction func = table.GetLuaFunction("Enable");
            if (func != null)
                func.Call(willShowPanel);
        }
        else
        {
            ResManager resMgr = GameManager.Instance.GetManager<ResManager>("ResManager");
            if (!syn)
            {
                resMgr.LoadPanelAsyn(bundleName, panelName, (obj)=>{
                    obj.transform.SetParent(PanelRoot,false);
                    if (!panelCache.ContainsKey(panelName))
                        panelCache.Add(panelName, obj);

                    obj.SetActive(true);
                    obj.name = panelName;
                    obj.AddComponent<LuaBehaviour>();
                    LuaFunction func = table.GetLuaFunction("OnCreate");
                    if (func != null)
                        func.Call(obj);
                    
                });
            }
            else
            {
                GameObject willShowPanel = resMgr.LoadPanelSyn(bundleName, panelName);
                if (!panelCache.ContainsKey(panelName))
                    panelCache.Add(panelName, willShowPanel);
                LuaFunction func = table.GetLuaFunction("OnCreate");
                if (func != null)
                    func.Call(willShowPanel);
            }
        }
    }

    public void HidePanel(string panelName)
    {
        GameObject curPanel = null;
        panelCache.TryGetValue(curOpenPanel, out curPanel);
        if (curPanel != null)
            curPanel.SetActive(false);
    }

    public void PushPopups(string popupsName)
    {

    }

}

