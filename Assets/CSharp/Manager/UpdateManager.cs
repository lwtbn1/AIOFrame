using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System;
/// <summary>
/// 资源更新管理器
/// </summary>
public class UpdateManager : MonoBehaviour {

    Dictionary<string, string> old_ver_dic;
    Dictionary<string, string> new_ver_dic;
    Dictionary<string, string> need_update_dic;

	void Start () {
        StartCoroutine(CheckUnPack());
	}
    string LatestFileList;
    string LatestVersion;
    string wwwResPath = "";
    IEnumerator CheckUpdate()
    {
        if (!Global.UpdateMode)
        {
            if (OnEndUpdate != null)
                OnEndUpdate();
            yield break;
        }
        Debug.Log("startchecke update......");

        WWW www = new WWW(NetConfig.ResUrl + "/LatestVersion.ini");
        while (!www.isDone)
            yield return new WaitForEndOfFrame();
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("11111 www.error : " + www.error);
            yield break;
        }
        LatestVersion = www.text.Trim();
        string nativeVersion = File.ReadAllText(Application.persistentDataPath + "/resVersion.ini").Trim();
        Debug.Log("ver : " + LatestVersion + "   " + nativeVersion);
        bool needUpdate = float.Parse(LatestVersion) > float.Parse(nativeVersion);
        
        if (!needUpdate)   
        {
            //不需要更新，做些其他操作
            if (OnEndUpdate != null)
                OnEndUpdate();
            yield break;
        }
        
        old_ver_dic = new Dictionary<string, string>();
        new_ver_dic = new Dictionary<string, string>();
        need_update_dic = new Dictionary<string, string>();
        wwwResPath = NetConfig.ResUrl + "/" + LatestVersion + "/" + GameDef.PackageRoot;
        www = new WWW(wwwResPath + "/filelist.txt");
        while (!www.isDone)
            yield return new WaitForEndOfFrame();
        byte[] bytes = www.bytes;
        string ver_new_str = Encoding.UTF8.GetString(bytes);
        string[] splits = ver_new_str.Split(new string[]{"\n"}, System.StringSplitOptions.None);
        for (int i = 0; i < splits.Length; i++)
        {
            string data_str = splits[i];
            if (data_str == null || data_str.Trim().Length == 0)
                continue;
            ClassCollections.VerData data = ClassCollections.VerData.ToObj(splits[i]);
            new_ver_dic.Add(data.res_name, data.md5);
        }
        LatestFileList = www.text;

        FileInfo fi_ver_old = new FileInfo(Application.persistentDataPath + "/" + GameDef.PackageRoot + "/filelist.txt");
        if (!fi_ver_old.Exists)
        {
            need_update_dic = new_ver_dic;
        }
        else
        {
            FileStream fs_ver_old = fi_ver_old.OpenRead();
            StreamReader sr_ver_old = new StreamReader(fs_ver_old);
            string line_data = null;
            while((line_data = sr_ver_old.ReadLine()) != null){
                if (line_data.Trim().Length == 0)
                    continue;
                ClassCollections.VerData data = ClassCollections.VerData.ToObj(line_data);
                old_ver_dic.Add(data.res_name, data.md5);
            }
            fs_ver_old.Close();
            sr_ver_old.Close();
            foreach (KeyValuePair<string, string> kv in new_ver_dic)
            {
                if (old_ver_dic.ContainsKey(kv.Key))
                {
                    if(!old_ver_dic[kv.Key].Equals(kv.Value))
                        need_update_dic.Add(kv.Key, kv.Value);
                }
                else
                {
                    need_update_dic.Add(kv.Key, kv.Value);
                }
            }
        }
        StartCoroutine(StartUpdate());
    }



    IEnumerator StartUpdate()
    {
        if (OnStartUpdate != null)
            OnStartUpdate();
        float ix = 0;
        foreach (KeyValuePair<string, string> kv in need_update_dic)
        {
            WWW www = new WWW(wwwResPath + "/" + kv.Key);
            while (!www.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            ix++;
            if (OnUpdating != null)
                OnUpdating(ix / (float)need_update_dic.Count);
            Debug.Log(ix / (float)need_update_dic.Count);
            byte[] bytes = www.bytes;
            string file_full_path = Application.persistentDataPath + "/" + GameDef.PackageRoot +"/" + kv.Key;
            string folder_path = Path.GetDirectoryName(file_full_path);
            if (!Directory.Exists(folder_path))
                Directory.CreateDirectory(folder_path);
            if (File.Exists(file_full_path))
                File.Delete(file_full_path);
            File.WriteAllBytes(file_full_path, bytes);
            yield return new WaitForEndOfFrame();
        }

        string resVersionPath = Application.persistentDataPath + "/resVersion.ini";
        if (File.Exists(resVersionPath))
            File.Delete(resVersionPath);
        File.WriteAllText(resVersionPath, LatestVersion, Encoding.UTF8);

        string filelistPath = Application.persistentDataPath + "/" + GameDef.PackageRoot + "/filelist.txt";
        if (File.Exists(filelistPath))
            File.Delete(filelistPath);
        File.WriteAllText(filelistPath,LatestFileList, Encoding.UTF8);

        if (OnEndUpdate != null)
            OnEndUpdate();
        
    }
    public Action OnStartUpdate;
    public Action<float> OnUpdating;
    public Action OnEndUpdate;

    IEnumerator CheckUnPack()
    {
        if (!Global.IsSandBoxMod)
        {
            StartCoroutine(CheckUpdate());
            yield break;
        }
            
        if (OnCheckUnpack != null)
            OnCheckUnpack();
        FileInfo p_ResVersionFile = new FileInfo(Application.persistentDataPath + "/resVersion.ini");
        
        string preResVersion = PlayerPrefs.GetString("resVersion");
        
        bool needUnPack = false;
        if (!p_ResVersionFile.Exists || string.IsNullOrEmpty(preResVersion))
        {
            needUnPack = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(preResVersion))
            {
                WWW www = new WWW(GameDef.StreamingAssetsPath + "/resVersion.ini");
                while (!www.isDone)
                    yield return new WaitForEndOfFrame();
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogError("www.error : " + www.error);
                    yield break;
                }
                string nowResVersion = www.text;
                www.Dispose();
                Debug.Log("version : " + nowResVersion + "   " + preResVersion);
                if (float.Parse(nowResVersion) < float.Parse(preResVersion))
                    needUnPack = true;
            }
        }
        Debug.Log("needUnpack ::::::::::::::: " + needUnPack);
        if (needUnPack)
            StartCoroutine(UnPack());
        else
            StartCoroutine(CheckUpdate());

    }

    IEnumerator UnPack()
    {
        if (OnStartUnPack != null)
            OnStartUnPack();
        PlayerPrefs.DeleteKey("resVersion");
        DirectoryInfo packageRoot = new DirectoryInfo(Application.persistentDataPath + "/" + GameDef.PackageRoot);
        if (packageRoot.Exists)
            packageRoot.Delete(true);
        packageRoot.Create();
        WWW www = new WWW(GameDef.StreamingAssetsPath + "/" + GameDef.PackageRoot + "/filelist.txt");
        while (!www.isDone)
            yield return new WaitForEndOfFrame();
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("www.error : " + www.error);
            yield break;
        }
        string filelistStr = www.text;
        File.WriteAllText(Application.persistentDataPath + "/" + GameDef.PackageRoot + "/filelist.txt", www.text, Encoding.UTF8);
        string[] filelistLines = File.ReadAllLines(Application.persistentDataPath + "/" + GameDef.PackageRoot + "/filelist.txt");
        for (var i = 0; i < filelistLines.Length; i++)
        {
            ClassCollections.VerData verData = ClassCollections.VerData.ToObj(filelistLines[i]);
            string fullName = Application.persistentDataPath + "/" + GameDef.PackageRoot + "/" + verData.res_name;
            string dir = Path.GetDirectoryName(fullName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            www = new WWW(GameDef.StreamingAssetsPath + "/" + GameDef.PackageRoot + "/" + verData.res_name);
            while (!www.isDone)
                yield return new WaitForEndOfFrame();
            File.WriteAllBytes(fullName, www.bytes);
            if (OnUnPacking != null)
                OnUnPacking((float)i / (float)filelistLines.Length);
            yield return new WaitForEndOfFrame();
        }
        www = new WWW(GameDef.StreamingAssetsPath + "/resVersion.ini");
        while (!www.isDone)
            yield return new WaitForEndOfFrame();
        File.WriteAllBytes(Application.persistentDataPath + "/resVersion.ini", www.bytes);
        PlayerPrefs.SetString("resVersion",www.text);
        if (OnEndUnPack != null)
            OnEndUnPack();
        StartCoroutine(CheckUpdate());
    }

    public Action OnCheckUnpack;
    public Action OnStartUnPack;
    public Action<float> OnUnPacking;
    public Action OnEndUnPack;

    public void AddAction(Action OnCheckUnpack, Action OnStartUnPack, Action<float> OnUnPacking, Action OnEndUnPack,
        Action OnStartUpdate, Action<float> OnUpdating, Action OnEndUpdate)
    {
        this.OnCheckUnpack = OnCheckUnpack;
        this.OnStartUnPack = OnStartUnPack;
        this.OnUnPacking = OnUnPacking;
        this.OnEndUnPack = OnEndUnPack;
        this.OnStartUpdate = OnStartUpdate;
        this.OnUpdating = OnUpdating;
        this.OnEndUpdate = OnEndUpdate;
    }

}
