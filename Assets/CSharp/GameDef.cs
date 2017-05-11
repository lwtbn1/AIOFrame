//**********************************************************************
// Name:        
// Author:      
// Version:     
// Date:        
// Description: 
//**********************************************************************
using UnityEngine;

public class GameDef {

    public static string PackageRoot
    {
        get
        {
#if UNITY_STANDALONE_WIN
            return "Win_Package";
#elif UNITY_ANDROID
            return "Android_Package";
#elif UNITY_IPHONE
            return "IOS_Package";
#endif
        }

    }
    //默认资源路径，也就是StreamingAssets，解包前
    public static string StreamingAssetsPath
    {
        get
        {
            string path = string.Empty;
            if(Application.platform == RuntimePlatform.Android)
                    path = "jar:file://" + Application.dataPath + "!/assets";
            else if(Application.platform == RuntimePlatform.IPhonePlayer)
                path = Application.dataPath + "/Raw";
            else
                path = Application.dataPath + "/" + "StreamingAssets";
            
            return path;
        }
    }


    public static string BundlePathRoot
    {
        get
        {
            if (Application.isMobilePlatform)
            {
                return "file:///" + Application.persistentDataPath + "/" + PackageRoot;
            }
            else
            {
                return Application.dataPath + "/StreamingAssets/" + PackageRoot;
            }
        }
    }
    public static string PanelPathRoot = string.Format(Application.dataPath + "/StreamingAssets/{0}/panel/", PackageRoot);
    public static string UIPathRoot = string.Format(Application.dataPath + "/StreamingAssets/{0}/ui/",PackageRoot);
    public static string EffectPathRoot = string.Format(Application.dataPath + "/StreamingAssets/{0}/effect",PackageRoot);
    public static string ModelPathRoot = string.Format(Application.dataPath + "/StreamingAssets/{0}/model", PackageRoot);

    public static string BundleExtName = ".pak";
	
}
