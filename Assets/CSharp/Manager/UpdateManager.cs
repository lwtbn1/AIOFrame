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
        if (Config.UpdateMode)
        {
            StartCoroutine(CheckUpdate());
        }
        else
        {
            if (OnEndUpdate != null)
                OnEndUpdate();
        }

	}
    byte[] new_ver_bytes;
    IEnumerator CheckUpdate()
    {
        Debug.Log("开始检查更新......");

        old_ver_dic = new Dictionary<string, string>();
        new_ver_dic = new Dictionary<string, string>();
        need_update_dic = new Dictionary<string, string>();

        WWW www = new WWW(NetConfig.ResUrl + "/version.ini");
        while (!www.isDone)
        {
            yield return 1;
        }
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
        new_ver_bytes = bytes;

        FileInfo fi_ver_old = new FileInfo(Application.persistentDataPath + "/version.ini");
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
            WWW www = new WWW( NetConfig.ResUrl + "/" + kv.Key);
            while (!www.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            ix++;
            if (OnUpdating != null)
                OnUpdating(ix / (float)need_update_dic.Count);
            Debug.Log(ix / (float)need_update_dic.Count);
            byte[] bytes = www.bytes;
            string file_full_path = Application.persistentDataPath + "/" + kv.Key;
            FileInfo file = new FileInfo(file_full_path);
            string folder_path =  file_full_path.Substring(0, file_full_path.LastIndexOf("/"));
            DirectoryInfo dir = new DirectoryInfo(folder_path);
            if (!dir.Exists)
                dir.Create();
            if (file.Exists)
                file.Delete();
            FileStream fs = file.Open(FileMode.CreateNew, FileAccess.Write);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }
        
        FileInfo fi_ver = new FileInfo(Application.persistentDataPath + "/version.ini");
        if (fi_ver.Exists)
            fi_ver.Delete();
        FileStream fs_ver = fi_ver.Open(FileMode.Create, FileAccess.ReadWrite);
        fs_ver.Write(new_ver_bytes, 0, new_ver_bytes.Length);
        fs_ver.Flush();
        fs_ver.Close();
        if (OnEndUpdate != null)
            OnEndUpdate();
        
    }
    public Action OnStartUpdate;
    public Action<float> OnUpdating;
    public Action OnEndUpdate;
    

}
