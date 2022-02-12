using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace AIOFrame.Util
{
    public class WWWUtil
    {

        private static WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        public static IEnumerator LoadFromStreamingAssetsPathASyn(string filePath, Action<string,byte[]> callBack)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(PathUtil.GetStreamingAssetsPath_WWW(filePath)))
            {
                req.SendWebRequest();
                while (!req.isDone)
                    yield return waitFrame;
                if (null != callBack)
                    callBack(req.downloadHandler.error, req.downloadHandler.data);
            }
        }
    }
}

