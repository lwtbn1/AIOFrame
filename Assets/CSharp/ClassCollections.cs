using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
public class ClassCollections
{
    public class VerData
    {
        public string res_name;
        public string md5;
        public VerData()
        {
        }
        public VerData(string res_name, string md5)
        {
            this.res_name = res_name;
            this.md5 = md5;
        }

        public string ToJson()
        {
            return JsonMapper.ToJson(this);
        }

        public static VerData ToObj(string json)
        {
            return JsonMapper.ToObject<VerData>(json);
        }
    }
}

