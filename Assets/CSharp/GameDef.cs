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
                return Application.persistentDataPath + "/" + PackageRoot;
            }
            else
            {
                return Application.dataPath + "/StreamingAssets/" + PackageRoot;
            }
        }
    }
    public static string PanelPathRoot = BundlePathRoot + "/panel/";
    public static string UIPathRoot = BundlePathRoot + "/ui/";
    public static string EffectPathRoot = BundlePathRoot + "/effect";
    public static string ModelPathRoot = BundlePathRoot + "/model";

    public static string BundleExtName = ".pak";

    public static string RawResourcesDir = Application.dataPath + "/RawResources";

    public enum PlatefromEnum
    {
        Windows = 0,
        Android,
        IOS,
    }

    public static string[] PlateformNames = new string[]{
        "Windows",
        "Android",
        "IOS",
    };

    public static PlatefromEnum CurrentPlateform
    {
        get
        {
#if UNITY_STANDALONE_WIN
            return PlatefromEnum.Windows;
#elif UNITY_ANDROID
            return PlatefromEnum.Android;
#elif UNITY_IPHONE
            return PlatefromEnum.IOS;
#endif
        }
    }

}
