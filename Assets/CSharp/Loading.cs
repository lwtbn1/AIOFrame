using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 1、卸载上一个 场景的特有bundle；
/// 2、加载本场景需要用到的特有bundle；
/// 3、异步加载下一个场景
/// </summary>
public class Loading : MonoBehaviour {
    
    
	// Use this for initialization
	void Start () { 
        SceneManager.LoadSceneAsync("", LoadSceneMode.Single);
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
