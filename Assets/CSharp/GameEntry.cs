//**********************************************************************
// Name:        
// Author:      
// Version:     
// Date:        
// Description: 
//**********************************************************************
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class GameEntry : MonoBehaviour {
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
    }

    bool m_networkConnected = false;
	// Use this for initialization
    IEnumerator Start()
    {
        StartCoroutine(CheckNetworkStatus());
        while (m_networkConnected == false)
        {
            yield return true;
        }

        gameObject.AddComponent<UpdateMgr>();
        enabled = false;

    }
    //检测网络版本
    IEnumerator CheckNetworkStatus()
    {
        yield return true;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            m_networkConnected = true;
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
            m_networkConnected = false;
        }
    }

}
