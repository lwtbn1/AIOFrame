using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleTonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T ins;
    private static bool isInit = false;
    public static T Ins
    {
        get {

            if(null == ins)
                Init();
            return ins;
        }
    }
    public static void Init()
    {
        if (!isInit)
        {
            var obj = new GameObject(typeof(T) + "(MonoSingleTon)");
            ins = obj.AddComponent<T>();
            DontDestroyOnLoad(ins);
        }
    }
}
