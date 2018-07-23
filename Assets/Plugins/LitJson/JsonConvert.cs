using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LitJson
{
    public static class JsonConvert
    {
        //public static string ToJsonString(this Dictionary<string, int> InDictionary)
        //{
        //    if (null == InDictionary) return string.Empty;
        //    else
        //    {


        //        return sb.ToString();
        //    }
        //}

        public static string ToJsonString<T>(this Dictionary<string, T> InDict) where T : struct
        {
            if (null == InDict) return string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder(512);
                sb.Append("[");

                Type type = typeof(T);

                string prefix = "{\"", middle, postfix;
                if (type.IsEnum)
                {
                    middle = "\":\"";
                    postfix = "\"},";
                }
                else
                {
                    middle = "\":";
                    postfix = "},";
                }
                foreach (KeyValuePair<string, T> itor in InDict)
                {
                    sb.Append(prefix)
                        .Append(itor.Key)
                        .Append(middle)
                        .Append(itor.Value)
                        .Append(postfix);
                }

                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                return sb.ToString();
            }
        }

        public static string ToJsonString(this Dictionary<string, string> InDict)
        {
            if (null == InDict) return string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder(512);
                sb.Append("[");

                foreach (KeyValuePair<string, string> itor in InDict)
                {
                    sb.Append("{\"")
                        .Append(itor.Key)
                        .Append("\":\"")
                        .Append(itor.Value)
                        .Append("\"},");
                }

                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                return sb.ToString();
            }
        }

        public static Dictionary<string, T> ToObject<T>(this JsonData InJsonData) where T : struct
        {
            try
            {
                int count = InJsonData.Count;
                Dictionary<string, T> dict = null;
                if (count > 0)
                {
                    dict = new Dictionary<string, T>(count);

                    Type type = typeof(T);
                    IList<KeyValuePair<string, JsonData>> itor;

                    if (type.IsEnum)
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            itor = InJsonData[i].ObjectList;
                            dict.Add(itor[0].Key, (T)Enum.Parse(type, itor[0].Value.ToString()));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            itor = InJsonData[i].ObjectList;
                            dict.Add(itor[0].Key, (T)Convert.ChangeType(itor[0].Value.ToString(), type));
                        }
                    }
                }
                return dict;
            }
            catch (Exception e)
            {
                Debug.LogError("Convert json to Dictionary error.\n" + e.Message + "\n" + e.StackTrace);
            }

            return null;
        }

        public static Dictionary<string, T> ToObject<T>(this string InJsonStr) where T : struct
        {
            Debug.LogError(InJsonStr);
            return ToObject<T>(JsonMapper.ToObject(InJsonStr));
        }

        public static Dictionary<string, string> ToObject(this JsonData InJsonData)
        {
            try
            {
                int count = InJsonData.Count;
                Dictionary<string, string> dict = null;
                if (count > 0)
                {
                    dict = new Dictionary<string, string>(count);

                    IList<KeyValuePair<string, JsonData>> itor;
                    
                    for (int i = 0; i < count; ++i)
                    {
                        itor = InJsonData[i].ObjectList;
                        dict.Add(itor[0].Key, itor[0].Value.ToString());
                    }
                }

                return dict;
            }
            catch (Exception e)
            {
                Debug.LogError("Convert json to Dictionary error.\n" + e.Message + "\n" + e.StackTrace);
            }

            return null;
        }

        public static Dictionary<string, string> ToObject(this string InJsonStr)
        {
            Debug.LogError(InJsonStr);
            return ToObject(JsonMapper.ToObject(InJsonStr));
        }

        public static string ToJsonString<T>(this T InT) where T : IJsonConverter, new()
        {
            return InT.ToJsonString();
        }

        public static T ToJsonObject<T>(this JsonData InJsonData) where T : IJsonConverter, new()
        {
            T t = new T();
            t.ToJsonObject(InJsonData);

            return t;
        }

        public static T ToJsonObject<T>(this string InJsonString) where T : IJsonConverter, new()
        {
            return ToJsonObject<T>(JsonMapper.ToObject(InJsonString));
        }
    }


}