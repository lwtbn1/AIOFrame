using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
/// 资源更新管理器
/// </summary>
public class UpdateMgr : MonoBehaviour {

    Dictionary<string, string> old_ver_dic;
    Dictionary<string, string> new_ver_dic;
    Dictionary<string, string> need_update_dic;
	void Start () {
        StartCoroutine(CheckUpdate());
	}

    IEnumerator CheckUpdate()
    {
        Debug.Log("开始检查更新......");

        old_ver_dic = new Dictionary<string, string>();
        new_ver_dic = new Dictionary<string, string>();
        need_update_dic = new Dictionary<string, string>();

        FileInfo fi_ver_new = new FileInfo(Application.persistentDataPath + "/version_new.ini");
        FileStream fs_ver_new = fi_ver_new.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
        fs_ver_new.Write(bytes, 0, bytes.Length);
        fs_ver_new.Flush();
        fs_ver_new.Close();


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
        yield return 1;
    }
}
