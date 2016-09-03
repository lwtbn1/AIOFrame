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

	// Use this for initialization
	void Start () {
	    WWW www = new WWW("http:/localhost:8080/slg");
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
