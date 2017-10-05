using System.IO;
using UnityEngine;

public static class JsonReader
{
    public static string GetJsonString(string path)
    {
        string jsonString = File.ReadAllText(path);
        return jsonString;
    }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string jsonString)
    {
        string newString = "{ \"array\": " + jsonString + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newString);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
