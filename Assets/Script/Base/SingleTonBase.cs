using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AIOFrame.Base
{
    public class SingleTonBase<T> where T : new()
    {
        private static Object lockObj = new Object();
        private static T ins;
        public static T Ins
        {
            get
            {
                lock (lockObj){
                    if (null == ins)
                        ins = new T();
                    return ins;
                }
            }
        }
    }
}

