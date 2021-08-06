using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Serializer
{
    // Start is called before the first frame update
    public static void Save(SaveObject so, string filename)
    {
        string json = JsonUtility.ToJson(so);

        File.WriteAllText(filename, json);

        Debug.Log(json);
    }

    public static T Load<T>(string filepath)
    {
        if (!File.Exists(filepath)) { return default; }

        string jsonLoaded = File.ReadAllText(filepath);

        return JsonUtility.FromJson<T>(jsonLoaded);
    }
}
