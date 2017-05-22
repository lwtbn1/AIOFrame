using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Global
{
    public static bool IsSandBoxMod = Application.isMobilePlatform;

    public static bool UpdateMode = false;   //是否开启更新模式



    private static GameObject panelRoot;
    public static GameObject PanelRoot
    {
        get
        {
            if (panelRoot == null)
                panelRoot = GameObject.Find("UIRoot/PanelRoot");
            return panelRoot;
        }
    }

    private static GameObject popupsRoot;
    public static GameObject PopupsRoot
    {
        get
        {
            if (popupsRoot == null)
                popupsRoot = GameObject.Find("UIRoot/PopupsRoot");
            return popupsRoot;
        }
    }

    private static GameObject tipsRoot;
    public static GameObject TipsRoot
    {
        get
        {
            if (tipsRoot == null)
                tipsRoot = GameObject.Find("UIRoot/TipsRoot");
            return tipsRoot;
        }
    }

}
