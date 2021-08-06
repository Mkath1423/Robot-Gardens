using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public string filename;
    public string Filepath { get { return Application.dataPath + "\\" + filename + ".txt"; } }

    public WorldInfo worldInfo;


    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists(Filepath))
        {
            Debug.Log("Found File");
            Load();
        }
        else
        {
            Debug.Log("Cound not find file. Loading default values...");
            worldInfo = new WorldInfo();
            
           
        }
        
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {

        // Serialize
        SaveObject saveObject = new LevelDataSaveObject()
        {
            worldInfoSaveObject = new WorldInfoSaveObject(worldInfo)
        };

        Serializer.Save(saveObject, Filepath);
    }

    private void Load()
    {
        LevelDataSaveObject saveObject = Serializer.Load<LevelDataSaveObject>(Filepath);
        
        worldInfo = new WorldInfo(saveObject.worldInfoSaveObject);
    }
}
