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
using System.Security.Cryptography;
using System.Text;
using System.Linq;
public class Build  {

    [MenuItem("Pack/AssetBundle/SetAssetBundlName")]
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

    [MenuItem("Pack/AssetBundle/Android")]
    static void CreateAssetBundle()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        string adrPath = Application.dataPath + "/StreamingAssets/adr_res";
        DirectoryInfo directoryInfo = new DirectoryInfo(adrPath);
        if (!directoryInfo.Exists)
            directoryInfo.Create();
        BuildPipeline.BuildAssetBundles(adrPath, BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Pack/gen_version/Android")]
    static void CreateVersion()
    {
        MoveLuaFile();
        FileInfo version_file = new FileInfo(Application.dataPath + "/StreamingAssets/adr_res/version.ini");
        if (version_file.Exists)
            version_file.Delete();
        FileStream fs = version_file.Open(FileMode.CreateNew, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        IEnumerable<string> fileList = Directory.GetFiles(Application.dataPath + "/StreamingAssets/adr_res", "*.*", SearchOption.AllDirectories).
            Where(s => s.EndsWith(".u3d") || s.EndsWith(".lua"));
        IEnumerator<string> enumerator = fileList.GetEnumerator();
        MD5 md5Hash = MD5.Create();
        while (enumerator.MoveNext())
        {
            string fileFullPath = enumerator.Current.Replace("\\", "/");
            FileInfo file = new FileInfo(fileFullPath);
            FileStream fileStream = file.OpenRead();
            byte[] md5HashBytes = md5Hash.ComputeHash(fileStream);
            int start_ix = fileFullPath.IndexOf("adr_res/") + "adr_res/".Length;
            string fileName = fileFullPath.Substring(start_ix);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5HashBytes.Length; i++)
                sb.Append(md5HashBytes[i].ToString("X2"));
            ClassCollections.VerData data = new ClassCollections.VerData(fileName, sb.ToString());
            string data_str = Encoding.UTF8.GetString(Encoding.Default.GetBytes(data.ToJson()));
            sw.WriteLine(data_str);
        }

        
        sw.Flush();
        fs.Close();
        sw.Close();
        AssetDatabase.Refresh();
    }
  
    static void MoveLuaFile()
    {
        DirectoryInfo lua_dir_old = new DirectoryInfo(Application.dataPath + "/StreamingAssets/adr_res/Lua");
        if (lua_dir_old.Exists)
            lua_dir_old.Delete(true);
        FileMgr.CopyDirectory(Application.dataPath + "/Lua", Application.dataPath + "/StreamingAssets/adr_res/Lua");
    }


}
