using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine.UI;
public class LuaBehaviour : MonoBehaviour
{
    private Dictionary<string, LuaFunction> buttons = new Dictionary<string, LuaFunction>();
    protected void Awake()
    {
        Util.CallMethod(name, "Awake", gameObject);
    }
    protected void OnEnable()
    {
        Util.CallMethod( name, "OnEnable", gameObject);
    }
    protected void Start()
    {
        Util.CallMethod(name, "Start");
    }
    //protected void Update()
    //{
    //    Util.CallMethod(name, "Update");
    //}
    //protected void LateUpdate()
    //{
    //    Util.CallMethod(name, "LateUpdate");
    //}
    
    protected void OnDisable()
    {
        Util.CallMethod(name, "OnDisable", gameObject);
    }
    protected void OnDestroy()
    {
        Util.CallMethod(name, "OnDestroy", gameObject);
    }
    protected void OnClick()
    {
        Util.CallMethod(name, "OnClick");
    }

    protected void OnClickEvent(GameObject go)
    {
        Util.CallMethod(name, "OnClick", go);
    }

    /// <summary>
    /// 添加单击事件
    /// </summary>
    public void AddClick(GameObject go, LuaFunction luafunc)
    {
        if (go == null || luafunc == null) return;
        buttons.Add(go.name, luafunc);
        go.GetComponent<Button>().onClick.AddListener(
            delegate()
            {
                luafunc.Call(go);
            }
        );
    }

    /// <summary>
    /// 删除单击事件
    /// </summary>
    /// <param name="go"></param>
    public void RemoveClick(GameObject go)
    {
        if (go == null) return;
        LuaFunction luafunc = null;
        if (buttons.TryGetValue(go.name, out luafunc))
        {
            luafunc.Dispose();
            luafunc = null;
            buttons.Remove(go.name);
        }
    }

    /// <summary>
    /// 清除单击事件
    /// </summary>
    public void ClearClick()
    {
        foreach (var de in buttons)
        {
            if (de.Value != null)
            {
                de.Value.Dispose();
            }
        }
        buttons.Clear();
    }

    //-----------------------------------------------------------------
    

    


}
