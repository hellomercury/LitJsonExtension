namespace LitJson
{
    public interface IJsonConverter
    {
        void ToJsonObject(JsonData InJsonData);
        string ToJsonString();
    }
}

