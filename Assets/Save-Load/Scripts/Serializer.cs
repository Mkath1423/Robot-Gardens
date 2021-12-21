using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;

public static class Serializer
{
    // Start is called before the first frame update
    public static void Save(SaveObject so, string filename)
    {
        string json = JsonUtility.ToJson(so);

        Debug.Log(json);

        File.WriteAllText(filename, json);
        /*
        using (FileStream fs = File.Create(Application.dataPath + "\\" + filename))
        {
            byte[] jsondata = new UTF8Encoding(true).GetBytes(json);

            fs.Write(jsondata, 0, jsondata.Length);
            //File.WriteAllText(filename, json);
        }
        */
    }

    public static T Load<T>(string filepath)
    {
        if (!File.Exists(filepath)) { return default; }

        string jsonLoaded = File.ReadAllText(filepath);

        return JsonUtility.FromJson<T>(jsonLoaded);
    }
}
