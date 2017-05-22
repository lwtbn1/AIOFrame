using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class PackageWindow : EditorWindow {

    string resVersionInput = "";
    int plateformSelect = 0;
    [MenuItem("Pack/PackCurrentPlatform")]
    public static void Show()
    {
        PackageWindow window = EditorWindow.GetWindowWithRect<PackageWindow>(new Rect(0f,0f,400f,200f),true,"打包",true);
        window.Show(true);
        window.ReadResVersion();
        window.plateformSelect = (int)GameDef.CurrentPlateform;
    }

    void OnGUI()
    {
        //打包平台
        plateformSelect = GUILayout.Toolbar(plateformSelect, GameDef.PlateformNames);

        GUILayout.Space(10);
        //版本号
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("资源版本");
        resVersionInput = GUILayout.TextField(resVersionInput, 10);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //打包
        //GUILayout.BeginHorizontal();
        //GUILayout.Space(150);
        if(GUILayout.Button("打包"))
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            if (string.IsNullOrEmpty(resVersionInput))
            {
                EditorUtility.DisplayDialog("警告！！！！", "请填写【资源版本】", "好的");
                return;
            }
            if ((int)GameDef.CurrentPlateform != plateformSelect)
            {
                bool ok = EditorUtility.DisplayDialog("警告！！！！", "您选中的平台和当前平台不相同，如果打包，会切换平台！！", "很确定","再检查下");
                Debug.Log(ok);
                if (ok)
                {
                    WriteResVersion();
                    EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargets[plateformSelect]);
                    EditorUserBuildSettings.activeBuildTargetChanged = () => { AssetBundlePackage.PackCurrentPlatform(); };
                }
            }
            else
            {
                WriteResVersion();
                AssetBundlePackage.PackCurrentPlatform();
            }
        }
        //GUILayout.Space(150);
        //GUILayout.EndHorizontal();
    }


    void ReadResVersion()
    {
        resVersionInput = "";
        FileInfo resVersion = new FileInfo(GameDef.RawResourcesDir + "/Config/resVersion.ini");
        if (!resVersion.Exists)
            return;
        using (Stream fileStream = resVersion.OpenRead())
        {
            using (StreamReader sr = new StreamReader(fileStream))
            {
                resVersionInput = sr.ReadLine();
                resVersionInput = resVersionInput == null ? "" : resVersionInput;
            }
        }
    }
    void WriteResVersion()
    {
        FileInfo resVersion = new FileInfo(GameDef.RawResourcesDir + "/Config/resVersion.ini");
        if (!resVersion.Exists)
            resVersion.Create();
        using (Stream stream = resVersion.OpenWrite())
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine(resVersionInput);
                sw.Flush();
            }
        }
        
    }
    BuildTarget[] buildTargets = new BuildTarget[] {BuildTarget.StandaloneWindows, BuildTarget.Android, BuildTarget.iOS };
}
