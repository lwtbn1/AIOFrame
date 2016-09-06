//**********************************************************************
// Name:        
// Author:      
// Version:     
// Date:        
// Description: 
//**********************************************************************
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Build  {

    [MenuItem("AssetBundle/SetAssetBundlName")]
    static void SetAssetBundleName()
    {
        //设置所有Panel的 assetBundle名
        string[] panelPaths = Directory.GetFiles("Assets/_Panel/", "*.prefab", SearchOption.AllDirectories);
        foreach (string path in panelPaths)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);
            string folderName = FileMgr.getFolderName(path);
            ai.assetBundleName = "panel/" + folderName + "/" + FileMgr.getFileName(path) + ".u3d";
        }
        //设置所有的ui的 assetBundle名(同一目录下的ui会放入一个图集，并打在一个包中)
        string[] uiPaths = Directory.GetFiles("Assets/_UI/", "*.png", SearchOption.AllDirectories);
        foreach (string path in uiPaths)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);
            ai.assetBundleName = "ui/" + FileMgr.getFolderName(path) + ".u3d";
        }
        //设置所有模型的 assetbundle名（同一目录下的model会被打入一个bundle内）
        string[] modelPaths = Directory.GetFiles("Assets/_Model/", "*.prefab", SearchOption.AllDirectories);
        foreach (string path in modelPaths)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);
            ai.assetBundleName = "model/" + FileMgr.getFolderName(path) + ".u3d";
        }
        //设置所有的特效的assetbundle名（同一目录下的特效会被打入一个bundle内）
        string[] effectPaths = Directory.GetFiles("Assets/_Effect/", "*.prefab", SearchOption.AllDirectories);
        foreach (string path in effectPaths)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);
            ai.assetBundleName = "effect/" + FileMgr.getFolderName(path) + ".u3d";
        }
    }

    [MenuItem("AssetBundle/Android")]
    static void CreateAssetBundle()
    {
        string adrPath = Application.dataPath + "/StreamingAssets/adr_res";
        DirectoryInfo directoryInfo = new DirectoryInfo(adrPath);
        if (!directoryInfo.Exists)
            directoryInfo.Create();
        BuildPipeline.BuildAssetBundles(adrPath, BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Pack/gen_version")]
    static void CreateVersion()
    {

    }
}
