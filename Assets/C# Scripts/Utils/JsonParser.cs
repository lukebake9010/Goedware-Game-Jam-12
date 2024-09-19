using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonParser
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

    public static T ParseJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, _settings);
    }

    public static string ParseEvent<T>(T @object)
    {
        return JsonConvert.SerializeObject(@object, _settings);
    }

    public static void TestSaveEvent<T>(T toSave, string name)
    {
        File.WriteAllText(Application.persistentDataPath + "/TEST/" + name, JsonConvert.SerializeObject(toSave, _settings));
    }
}
