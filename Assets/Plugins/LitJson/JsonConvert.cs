using System.Collections.Generic;
using System.Text;

namespace LitJson
{
    public static class JsonConvert
    {
        public static string ToJsonString(this Dictionary<string, int> InDictionary)
        {
            if (null == InDictionary) return string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder(512);
                sb.Append("[");
                foreach (KeyValuePair<string, int> itor in InDictionary)
                {
                    sb.Append("[")
                        .Append(itor.Key)
                        .Append(",")
                        .Append(itor.Value)
                        .Append("]")
                        .Append(",");
                }
                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                return sb.ToString();
            }
        }

        public static Dictionary<string, int> ToObject(this JsonData InJsonData)
        {
            return null;
        }
        public static Dictionary<string, int> ToObject(this string InJsonStr)
        {
            return null;
        } 
    }


}