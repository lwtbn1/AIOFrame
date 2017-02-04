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
using UnityEngine.UI;
public class GameEntry : MonoBehaviour {
    public Slider updateSlider;
    public Text infoText;
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

        UpdateMgr updateMgr = gameObject.AddComponent<UpdateMgr>();
        updateMgr.OnStartUpdate = () =>
        {
            updateSlider.gameObject.SetActive(true);
            updateSlider.value = 0;
            infoText.text = "正在更新....";
        };
        updateMgr.OnUpdating = (o) => {
            updateSlider.value = o;
        };
        updateMgr.OnEndUpdate = () => {
            updateSlider.value = 1;
            gameObject.AddComponent<GameMain>();
        };
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
