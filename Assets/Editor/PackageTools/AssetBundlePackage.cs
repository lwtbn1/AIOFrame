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
public class AssetBundlePackage  {

    public static BuildTarget CurBuildTarget
    {
        get
        {
#if UNITY_STANDALONE_WIN
            return BuildTarget.StandaloneWindows;
#elif UNITY_ANDROID
            return BuildTarget.Android;
#elif UNITY_IPHONE
            return BuildTarget.iOS;
#endif
        }
    }


    static BundlesConfig GetBundlesConfig()
    {
        FileInfo file = new FileInfo(Application.dataPath + "/Editor/PackageTools/BundlesConfig.txt");
        StreamReader sr = new StreamReader(file.OpenRead());
        string config =  sr.ReadToEnd();
        BundlesConfig bundlesConfig = LitJson.JsonMapper.ToObject<BundlesConfig>(config);
        sr.Close();
        return bundlesConfig;
    }
    static AssetBundleBuild[] GetAssetBundleBuilds(BundlesConfig bundlesConfig)
    {
        List<AssetBundleBuild> list = new List<AssetBundleBuild>();
        for (var i = 0; i < bundlesConfig.bundles.Length; i++)
        {
            AssetBundleBuild[] array = null;
            AssetBundlePackageInfo bundleInfo = bundlesConfig.bundles[i];
            switch (bundleInfo.packageType)
            {
                case "Dir":
                    array = GetOneDir(bundleInfo);
                    break;
                case "Dir_Dir":
                    array = GetOneDir_Dirs(bundleInfo);
                    break;
                case "Dir_File":
                    array = GetOneDir_Files(bundleInfo);
                    break;
                case "File":
                    array = GetOneFile(bundleInfo);
                    break;
            }
            if (array != null)
                list.AddRange(array);
        }
        return list.ToArray();
    }

    public static void PackCurrentPlatform()
    {
        //打资源包
        BundlesConfig bundlesConfig = GetBundlesConfig();
        AssetBundleBuild[] builds = GetAssetBundleBuilds(bundlesConfig);
        string outputPath = Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot;
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);
        BuildPipeline.BuildAssetBundles(outputPath, builds, BuildAssetBundleOptions.None, CurBuildTarget);
        //复制Lua脚本
        MoveLuaFile();
        //复制config
        MoveConfig();
        //创建FileList
        CreateFileList();

        AssetDatabase.Refresh();
    }

    static void CreateFileList()
    {
        FileInfo fileList = new FileInfo(Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot + "/filelist.txt");
        if (fileList.Exists)
            fileList.Delete();
        StringBuilder fileListInfo = new StringBuilder();
        IEnumerable<string> files = Directory.GetFiles(Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot, "*.*", SearchOption.AllDirectories).
            Where(s => !s.EndsWith(".meta") && !s.EndsWith(".manifest"));
        IEnumerator<string> enumerator = files.GetEnumerator();
        MD5 md5Hash = MD5.Create();
        while (enumerator.MoveNext())
        {
            string fileFullPath = enumerator.Current.Replace("\\", "/");
            FileInfo file = new FileInfo(fileFullPath);
            FileStream fileStream = file.OpenRead();
            byte[] md5HashBytes = md5Hash.ComputeHash(fileStream);
            int start_ix = fileFullPath.IndexOf(GameDef.PackageRoot + "/") + (GameDef.PackageRoot + "/").Length;
            string fileName = fileFullPath.Substring(start_ix);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5HashBytes.Length; i++)
                sb.Append(md5HashBytes[i].ToString("X2"));
            ClassCollections.VerData data = new ClassCollections.VerData(fileName, sb.ToString());
            string data_str = Encoding.UTF8.GetString(Encoding.Default.GetBytes(data.ToJson()));
            fileListInfo.AppendLine(data_str);
        }
        
        FileStream fs = fileList.Open(FileMode.CreateNew, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(fileListInfo.ToString());
        sw.Flush();
        fs.Close();
        sw.Close();
        AssetDatabase.Refresh();
    }
  
    static void MoveLuaFile()
    {
        DirectoryInfo lua_dir_old = new DirectoryInfo(Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot + "/Lua");
        if (lua_dir_old.Exists)
            lua_dir_old.Delete(true);
        string luaSrcDir = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets")) + "/LuaProj/src";
        FileUtil.CopyDir(luaSrcDir, Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot + "/Lua", new List<string>() { "meta", "manifest"});

        DirectoryInfo tolua_dir_old = new DirectoryInfo(Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot + "/ToLua");
        if (tolua_dir_old.Exists)
            tolua_dir_old.Delete(true);
        string toluaSrcDir = Application.dataPath + "/ToLua/Lua";
        FileUtil.CopyDir(toluaSrcDir, Application.dataPath + "/StreamingAssets/" + GameDef.PackageRoot + "/ToLua", new List<string>() { "meta", "manifest" });
    }
    static void MoveConfig()
    {
        FileUtil.CopyDir(GameDef.RawResourcesDir + "/Config", Application.dataPath + "/StreamingAssets", new List<string>() { "meta", "manifest" });
    }
    
    /// <summary>
    /// 得到一个文件的ABB
    /// </summary>
    static AssetBundleBuild[] GetOneFile(AssetBundlePackageInfo bundleInfo)
    {
        Object rObj = AssetDatabase.LoadAssetAtPath(bundleInfo.assetPath, typeof(Object));
        if (rObj == null) return null;

        AssetBundleBuild rABB = new AssetBundleBuild();
        rABB.assetBundleName = bundleInfo.name + GameDef.BundleExtName;
        rABB.assetNames = new string[] { bundleInfo.assetPath };
        return new AssetBundleBuild[] { rABB };
    }
    
    /// <summary>
    /// 得到一个目录的ABB
    /// </summary>
    static AssetBundleBuild[] GetOneDir(AssetBundlePackageInfo bundleInfo)
    {
        DirectoryInfo rDirInfo = new DirectoryInfo(bundleInfo.assetPath);
        if (!rDirInfo.Exists) return null;

        AssetBundleBuild rABB = new AssetBundleBuild();
        rABB.assetBundleName = bundleInfo.name + GameDef.BundleExtName;
        rABB.assetNames = new string[] { bundleInfo.assetPath };
        return new AssetBundleBuild[] { rABB };
    }

    /// <summary>
    /// 得到一个目录下的所有的文件对应的ABB
    /// </summary>
    static AssetBundleBuild[] GetOneDir_Files(AssetBundlePackageInfo bundleInfo)
    {
        DirectoryInfo rDirInfo = new DirectoryInfo(bundleInfo.assetPath);
        if (!rDirInfo.Exists) return null;

        List<string> allABPaths = new List<string>();
        FileUtil.RecursiveDir(bundleInfo.assetPath, ref allABPaths);

        List<AssetBundleBuild> rABBList = new List<AssetBundleBuild>();

        for (int i = 0; i < allABPaths.Count; i++)
        {
            string rAssetPath = allABPaths[i];
            AssetBundleBuild rABB = new AssetBundleBuild();
            rABB.assetBundleName = bundleInfo.name + "/" + Path.GetFileNameWithoutExtension(rAssetPath) + GameDef.BundleExtName;
            rABB.assetNames = new string[] { rAssetPath }; 
            rABBList.Add(rABB);
        }

        return rABBList.ToArray();

    }

    /// <summary>
    /// 得到一个目录下的所有的目录对应的ABB
    /// </summary>
    static AssetBundleBuild[] GetOneDir_Dirs(AssetBundlePackageInfo bundleInfo)
    {
        DirectoryInfo rDirInfo = new DirectoryInfo(bundleInfo.assetPath);
        if (!rDirInfo.Exists) return null;

        List<AssetBundleBuild> rABBList = new List<AssetBundleBuild>();
        DirectoryInfo[] rSubDirs = rDirInfo.GetDirectories();
        for (int i = 0; i < rSubDirs.Length; i++)
        {
            string rDirPath = rSubDirs[i].FullName;
            string rFileName = "";
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                string rRootPath = System.Environment.CurrentDirectory + "\\";
                rDirPath = rDirPath.Replace(rRootPath, "").Replace("\\", "/");
                rFileName = Path.GetFileNameWithoutExtension(rDirPath);
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string rRootPath = System.Environment.CurrentDirectory + "/";
                rDirPath = rDirPath.Replace(rRootPath, "");
                rFileName = Path.GetFileNameWithoutExtension(rDirPath);
            }
            else
            {
                throw new System.NotSupportedException("当前运行平台不支持打包操作");
            }

            AssetBundleBuild rABB = new AssetBundleBuild();
            rABB.assetBundleName = bundleInfo.name + "/" + rFileName +GameDef.BundleExtName;
            rABB.assetNames = new string[] { rDirPath };
            rABBList.Add(rABB);
        }
        return rABBList.ToArray();
    }
        

}
