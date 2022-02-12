using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace AIOFrame.Util
{
    public class PathUtil
    {
#if UNITY_ANDROID
        public static string StreamingAssetsPath_WWW = Application.streamingAssetsPath;
#else
        public static string StreamingAssetsPath_WWW = Application.streamingAssetsPath;
#endif

        public static string GetStreamingAssetsPath_WWW(string filePath)
        {
            return string.Format("{0}/{1}", StreamingAssetsPath_WWW, filePath);
        }

#if UNITY_ANDROID
        public static string PersistentDataPath_WWW = "jar:file://" + Application.persistentDataPath;
#else
        public static string PersistentDataPath_WWW = "file:///" + Application.persistentDataPath;
#endif
    }
}

