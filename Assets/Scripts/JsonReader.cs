using System.IO;
using UnityEngine;

public static class JsonReader
{
    public static string GetJsonString(string path)                             //reads the json file and returns its content. uses System.IO
    {
        string jsonString = File.ReadAllText(path);
        return jsonString;
    }
}

public class JsonHelper                                                         //has methods to deal with jsons
{
    public static T[] getJsonArray<T>(string jsonString)                        //enables parsing arrays of objects from json
    {
        string newString = "{ \"array\": " + jsonString + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newString);       //parses a json string into a wrapper object
        return wrapper.array;                                                   //..that has an array member
    }

    [System.Serializable]
    private class Wrapper<T>                                                    //an intermediate class that has generic array member
    {
        public T[] array = null;
    }
}
