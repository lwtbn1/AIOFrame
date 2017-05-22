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
    public GameObject ProgressPanel;
    public Slider slider;
    public Text infoText;
    
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
        if (ProgressPanel == null)
        {
            ProgressPanel = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("ProgressPanel"));
            UIUtil.PanelSetParent(Global.PanelRoot, ProgressPanel);
        }
        slider = UIUtil.FindInChild(ProgressPanel, "slider").GetComponent<Slider>();
        infoText = UIUtil.FindInChild(ProgressPanel, "infoText").GetComponent<Text>();
        
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
        updateMgr.AddAction(OnCheckUnpack, OnStartUnPack, OnUnPacking, OnEndUnPack, OnStartUpdate, OnUpdating, OnEndUpdate);
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

    void OnCheckUnpack()
    {
        slider.gameObject.SetActive(true);
        slider.value = 0;
        infoText.text = "启动游戏检查....";
    }
    void OnStartUnPack()
    {
        slider.gameObject.SetActive(true);
        slider.value = 0;
        infoText.text = "首次运行，开始解包....";
    }

    void OnUnPacking(float val)
    {
        slider.value = val;
        infoText.text = "首次运行，开始解包...." + (val * 100f) + "%";
    }
    void OnEndUnPack(){
        slider.value = 1;
        infoText.text = "解包完成";
        ProgressPanel.SetActive(false);
    }

    void OnStartUpdate()
    {
        slider.gameObject.SetActive(true);
        slider.value = 0;
        infoText.text = "正在更新....";
    }
    void OnUpdating(float o)
    {
        slider.value = o;
    }
    void OnEndUpdate(){
        slider.value = 1;
        ProgressPanel.SetActive(false);
        GameManager.Instance.AddManager<ResManager>("ResManager");
        GameManager.Instance.AddManager<UIManager>("UIManager");
        GameManager.Instance.AddManager<NetworkManager>("NetworkManager");
        GameManager.Instance.AddManager<LuaManager>("LuaManager");
    }

}
