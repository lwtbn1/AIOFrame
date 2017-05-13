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
    public GameObject panel_update;
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
        UpdateManager updateMgr = GameManager.Instance.AddManager<UpdateManager>("UpdateManager") as UpdateManager;

        updateMgr.OnCheckUnpack = () =>
        {
            updateSlider.gameObject.SetActive(true);
            updateSlider.value = 0;
            infoText.text = "启动游戏检查....";
        };
        updateMgr.OnStartUnPack = () => {
            updateSlider.gameObject.SetActive(true);
            updateSlider.value = 0;
            infoText.text = "首次运行，开始解包....";
        };

        updateMgr.OnUnPacking = (val) =>
        {
            updateSlider.value = val;
            infoText.text = "首次运行，开始解包...." + (val * 100f) + "%";
        };
        updateMgr.OnEndUnPack = () => {
            updateSlider.value = 1;
            infoText.text = "解包完成";
            panel_update.SetActive(false);
        };

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
            panel_update.SetActive(false);
            GameManager.Instance.AddManager<ResManager>("ResManager");
            GameManager.Instance.AddManager<UIManager>("UIManager");
            GameManager.Instance.AddManager<NetworkManager>("NetworkManager");
            GameManager.Instance.AddManager<LuaManager>("LuaManager");
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
