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

public class ResMgr {
    private static Dictionary<string, AssetBundle> ui_bundles = new Dictionary<string, AssetBundle>();
    private static Dictionary<string, AssetBundle> panel_bundles = new Dictionary<string, AssetBundle>();
    /// <summary>
    /// 同步加载图片
    /// </summary>
    /// <param name="atlas">图集名</param>
    /// <param name="sp_name">图名</param>
    /// <returns></returns>
    public static Sprite LoadSpriteSyn(string atlas, string sp_name)
    {
        AssetBundle bundle = getUIBundle(atlas);
        if (bundle == null)
            bundle = AssetBundle.LoadFromFile(GameDef.UIPathRoot + "/" + atlas + ".u3d");
        cacheUIBundle(atlas, bundle);
        Sprite sp = bundle.LoadAsset<Sprite>(sp_name);
        return sp;
    }

    /// <summary>
    /// 异步加载图片
    /// </summary>
    /// <param name="atlas">图集名</param>
    /// <param name="sp_name">图片名</param>
    /// <param name="callBack">加载后执行</param>
    public static IEnumerator LoadSpriteAsyn(string atlas, string sp_name, Action<Sprite> callBack)
    {
        AssetBundle bundle = getUIBundle(atlas);
        if(bundle == null){
            AssetBundleCreateRequest bundleReq = AssetBundle.LoadFromFileAsync(GameDef.UIPathRoot + "/" + atlas + ".u3d");
            while (!bundleReq.isDone)
                yield return false;
            bundle = bundleReq.assetBundle;
            
        }
        cacheUIBundle(atlas, bundle);
        Sprite sp = bundle.LoadAsset<Sprite>(sp_name);
        if(callBack != null)
            callBack(sp);
    }
    /// <summary>
    /// 切换场景时，加载该场景需要用到的bundle，一般这里只会加载通用的部分
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IEnumerator LoadUIBundlesAsyn(List<string> list)
    {
        for(int i = 0; i < list.Count; i++){
            string atlas = list[i];
            if (ui_bundles.ContainsKey(atlas))
                continue;
            AssetBundle bundle = getUIBundle(atlas);
            if (bundle == null)
            {
                AssetBundleCreateRequest bundleReq = AssetBundle.LoadFromFileAsync(GameDef.UIPathRoot + "/" + atlas + ".u3d");
                while (!bundleReq.isDone)
                    yield return false;
                bundle = bundleReq.assetBundle;
            }
            cacheUIBundle(atlas, bundle);
            yield return true;
        }
        
    }
    /// <summary>
    /// 切换场景时，释放当前场景加载的bundle
    /// </summary>
    /// <param name="list"></param>
    public static void UnLoadUIBundles(List<string> list)
    {
        for(int i = 0 ; i < list.Count; i++)
            UnloadUIBundle(list[i], true);
        
    }
    /// <summary>
    /// 同步加载panel
    /// </summary>
    /// <param name="panel_name"></param>
    /// <returns></returns>
    public static GameObject LoadPanelSyn(string panel_name)
    {
        AssetBundle bundle = getPanelBundle(panel_name);
        if (bundle == null)
            bundle = AssetBundle.LoadFromFile(GameDef.PanelPathRoot + "/" + panel_name + ".u3d");
        cachePanelBundle(panel_name, bundle);
        GameObject panel = bundle.LoadAsset<GameObject>(panel_name);
        return panel;
    }
    /// <summary>
    /// 异步加载panel
    /// </summary>
    /// <param name="panel_name"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public static IEnumerator LoadPanelAsyn(string panel_name, Action<GameObject> callBack)
    {
        AssetBundle bundle = getPanelBundle(panel_name);
        if (bundle == null)
        {
            AssetBundleCreateRequest bundleReq = AssetBundle.LoadFromFileAsync(GameDef.UIPathRoot + panel_name + ".u3d");
            while (!bundleReq.isDone)
                yield return false;
            bundle = bundleReq.assetBundle;

        }
        cacheUIBundle(panel_name, bundle);
        GameObject panel = bundle.LoadAsset<GameObject>(panel_name);
        if (callBack != null)
            callBack(panel);
    }

    /// <summary>
    /// 缓存加载出来的bundle
    /// </summary>
    /// <param name="name"></param>
    /// <param name="bundle"></param>
    private static void cacheUIBundle(string name, AssetBundle bundle)
    {
        if (ui_bundles.ContainsKey(name))
            return;
        ui_bundles.Add(name, bundle);
    }
    /// <summary>
    /// 从缓存中获取bundle
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AssetBundle getUIBundle(string name)
    {
        AssetBundle bundle = null;
        if (ui_bundles.ContainsKey(name))
            bundle = ui_bundles[name];
        return bundle;
    }
    /// <summary>
    /// 卸载bundle
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unloadAllLoadedObjects"></param>
    public static void UnloadUIBundle(string name, bool unloadAllLoadedObjects = false)
    {
        if (ui_bundles.ContainsKey(name))
        {
            ui_bundles[name].Unload(unloadAllLoadedObjects);
        }
            

    }
    /// <summary>
    /// 缓存加载出来的bundle
    /// </summary>
    /// <param name="name"></param>
    /// <param name="bundle"></param>
    private static void cachePanelBundle(string name, AssetBundle bundle)
    {
        if (panel_bundles.ContainsKey(name))
            return;
        panel_bundles.Add(name, bundle);
    }
    /// <summary>
    /// 从缓存中获取bundle
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AssetBundle getPanelBundle(string name)
    {
        AssetBundle bundle = null;
        if (panel_bundles.ContainsKey(name))
            bundle = panel_bundles[name];
        return bundle;
    }
    /// <summary>
    /// 卸载bundle
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unloadAllLoadedObjects"></param>
    public static void UnloadPanelBundl(string name, bool unloadAllLoadedObjects = false)
    {
        if (panel_bundles.ContainsKey(name))
        {
            panel_bundles[name].Unload(unloadAllLoadedObjects);
        }


    }
}