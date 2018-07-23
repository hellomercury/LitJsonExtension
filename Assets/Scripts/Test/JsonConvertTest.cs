using System.Collections;
using System.Collections.Generic;
using LitJson;
using Newtonsoft.Json.Linq;
using UnityEngine;
public enum myEnum
{
    a,
    b,
    c,
    d
}
public class JsonConvertTest : MonoBehaviour
{
    // Use this for initialization
    void OnGUI()
    {
        if (GUILayout.Button("<int>"))
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("a", 0);
            dict.Add("b", 1);
            dict.Add("c", 2);
            dict.Add("d", 3);
            dict.Add("e", 4);
            dict.Add("f", 5);
            dict = dict.ToJsonString().ToObject<int>();

            foreach (KeyValuePair<string, int> itor in dict)
            {
                Debug.LogError(itor.Key + "," + itor.Value);
            }
        }

        if (GUILayout.Button("<Enum>"))
        {
            Dictionary<string, myEnum> dict = new Dictionary<string, myEnum>();
            dict.Add("a", myEnum.a);
            dict.Add("b", myEnum.b);
            dict.Add("c", myEnum.c);
            dict.Add("d", myEnum.d);
            dict = dict.ToJsonString().ToObject<myEnum>();

            foreach (KeyValuePair<string, myEnum> itor in dict)
            {
                Debug.LogError(itor.Key + "," + itor.Value);
            }
        }

        if (GUILayout.Button("<string>"))
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("a", "aa");
            dict.Add("b", "bb");
            dict.Add("c", "cc");
            dict.Add("d", "dd");
            dict = dict.ToJsonString().ToObject();

            foreach (KeyValuePair<string, string> itor in dict)
            {
                Debug.LogError(itor.Key + "," + itor.Value);
            }
        }

        if (GUILayout.Button("<JsonConvertTest>"))
        {
            Teee eee = new Teee();
            eee.a = 1;
            eee.b = "a";
            eee.tees = new List<Tee>(19);
            for (int i = 0; i < 13; ++i)
            {
                Tee t = new Tee();
                t.c = i;
                t.d = i.ToString();
                eee.tees.Add(t);
            }
            Debug.LogError(eee.ToJsonString());
            Debug.LogError(eee.ToJsonString().ToJsonObject<Teee>());
        }

        if (GUILayout.Button("ddddddddddddd"))
        {
            JObject rss = new JObject(
                new JProperty("appVersion", "sss"),
                new JProperty("model", "aaa"),
                new JProperty("osVersion", "dddd"),
                new JProperty("platform", "eee"),
                new JProperty("rate", "asd"),
                new JProperty("comment", "dddd")

            );

            Debug.LogError(rss.ToString());
        }

        if (GUILayout.Button("To Json / String"))
        {
            Tee t = new Tee();
            t.c = 3;
            t.d = "aaaa";
            Debug.LogError("Json = " + t.ToJsonData().ToJson());
            Debug.LogError("string = " + t.ToJsonData().ToString());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Teee : IJsonConverter
{
    public int a;
    public string b;
    public List<Tee> tees;

    public void ToJsonObject(JsonData InJsonData)
    {
        a = (int)InJsonData["a"];
        b = (string)InJsonData["b"];
        int count = InJsonData["tees"].Count;
        if (count > 0)
        {
            tees = new List<Tee>(count);
            for (int i = 0; i < count; ++i)
            {
                Debug.LogError(InJsonData["tees"][i].ToJson());
                tees.Add(InJsonData["tees"][i].ToJsonObject<Tee>());
            }
        }
    }

    public string ToJsonString()
    {
        JsonData jd = new JsonData();
        jd["a"] = a;
        jd["b"] = b;
        jd["tees"] = new JsonData();
        if (null != tees)
        {
            for (int i = 0; i < tees.Count; ++i)
            {
                jd["tees"].Add(tees[i].ToJsonData());
            }
        }

        Debug.LogError(jd.ToJson());
        return jd.ToJson();
    }

    public override string ToString()
    {
        string str = string.Empty;
        if (tees != null)
            for (int i = 0; i < tees.Count; ++i)
            {
                str += tees[i] + ",";
            }
        return "a = " + a + "\nb = " + b + "\ntees =  " + str;
    }
}

public class Tee : IJsonConverter
{
    public int c;
    public string d;

    public void ToJsonObject(JsonData InJsonData)
    {
        c = (int)InJsonData["c"];
        d = (string)InJsonData["d"];
    }

    JsonData jd = new JsonData();
    public JsonData jsonData
    {
        get
        {
            JsonData jd = new JsonData();
            jd["c"] = c;
            jd["d"] = d;
            return jd;
        }
    }

    public string ToJsonString()
    {
        return ToJsonData().ToJson();
    }

    public JsonData ToJsonData()
    {
        JsonData jd = new JsonData();
        jd["c"] = c;
        jd["d"] = d;
        return jd;
    }
}