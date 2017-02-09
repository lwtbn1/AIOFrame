using UnityEngine;
using System.Collections;

public class LuaHelper {

    public static ResMgr GetResManager()
    {
        return GameMgr.Instance.GetManager<ResMgr>("ResMgr");
    }

    public static UIMgr GetUIManager()
    {
        return GameMgr.Instance.GetManager<UIMgr>("UIMgr");
    }
}
