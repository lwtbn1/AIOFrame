using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotFixProj.UI
{
    public class UIStack
    {
        public static Stack<UIGroup> stacks = new Stack<UIGroup>();
        public static void Push(string uiGroupName)
        {
            UIGroup curUIGroup = stacks.Pop();
            curUIGroup.OnPop();
            UIGroup pushUIGroup = null;
            if (GroupDefine.uis.TryGetValue(uiGroupName, out pushUIGroup))
            {
                pushUIGroup.OnPush();
                stacks.Push(pushUIGroup);
            }
            else
            {
                Debug.LogError("打开的面板不存在！！" + uiGroupName);
            }
                
        }
        
        /// <summary>
        /// UI的状态
        /// </summary>
        public enum E_UIState
        {
            Default,        //默认    
            Pushing,        //正在打开
            Showing,        //显示中
            Hide,           //隐藏
            Destroyed,      //销毁
        }
    }
}
