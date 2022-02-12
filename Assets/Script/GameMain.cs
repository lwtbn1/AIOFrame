using AIOFrame.Mgr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    private void Awake()
    {
        ILRuntimeMgr.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Ins.LoadDll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
