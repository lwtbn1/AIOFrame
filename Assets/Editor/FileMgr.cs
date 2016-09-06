﻿//**********************************************************************
// Name:        
// Author:      
// Version:     
// Date:        
// Description: 
//**********************************************************************
//**********************************************************************
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class FileMgr : MonoBehaviour {
    public static string getFileName(string path)
    {
        string filePath = path.Replace("\\", "/");
        int start = filePath.LastIndexOf("/");
        int end = filePath.LastIndexOf(".");
        string fileName = filePath.Substring(start + 1, end - start - 1);
        return fileName;
    }
    public static string getFolderName(string path)
    {
        string filePath = path.Replace("\\", "/");
        char[] separator = {'/' };
        string[] splits = filePath.Split(separator);
        return splits[splits.Length - 2];
    }

    public static string GetMD5()
    {
        string md5 = "";
        return md5;
    }


}