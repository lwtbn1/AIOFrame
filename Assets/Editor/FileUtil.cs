//**********************************************************************
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
public class FileUtil : MonoBehaviour {


    /// <summary>
    /// 递归获得path下所有文件的全路径
    /// </summary>
    /// <param name="path"></param>
    /// <param name="allFilePath"></param>
    /// <param name="isFirstRun"></param>
    public static void RecursiveDir(string path, ref List<string> allFilePath, bool isFirstRun = true)
    {
        if (isFirstRun && allFilePath.Count > 0)
        {
            allFilePath.TrimExcess();
            allFilePath.Clear();
        }

        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);


        foreach (string dir in dirs)
        {
            RecursiveDir(dir, ref allFilePath, false);
        }
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;

            allFilePath.Add(filename.Replace('\\', '/'));
        }
    }

    /// <summary>
    /// 复制目录
    /// </summary>
    /// <param name="sourceDir"></param>
    /// <param name="desDir"></param>
    /// <param name="kicks"></param>
    public static void CopyDir(string sourceDir, string desDir, List<string> kicks)
    {
        DirectoryInfo sourceDirInfo = new DirectoryInfo(sourceDir);
        DirectoryInfo desDirInfo = new DirectoryInfo(desDir);

        if (!sourceDirInfo.Exists)
            return;
        if (!desDirInfo.Exists)
            desDirInfo.Create();

        DirectoryInfo[] dirInfos = sourceDirInfo.GetDirectories();
        if (dirInfos != null && dirInfos.Length > 0)
        {
            for (var i = 0; i < dirInfos.Length; i++)
                CopyDir(dirInfos[i].FullName, desDir + "/" + dirInfos[i].Name, kicks);
        }

        FileInfo[] fileInfos = sourceDirInfo.GetFiles();
        if (fileInfos != null && fileInfos.Length > 0)
        {
            for (var i = 0; i < fileInfos.Length; i++)
            {
                if (kicks != null && !kicks.Contains(fileInfos[i].Extension))
                    File.Copy(fileInfos[i].FullName, desDir + "/" + fileInfos[i].Name, true);
            }
                
        }
    }
}
