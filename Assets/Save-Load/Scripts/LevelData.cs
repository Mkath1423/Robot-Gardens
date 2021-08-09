using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public string filename;
    public string Filepath { get { return Application.dataPath + "\\" + filename + ".txt"; } }

    public TilemapWithInfo worldTiles;

    public List<Garden> gardens;

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
            worldTiles = new TilemapWithInfo();
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
            tilemapWithInfoSaveObject = new TilemapWithInfoSaveObject(worldTiles)
        };

        Serializer.Save(saveObject, Filepath);
    }

    private void Load()
    {
        LevelDataSaveObject saveObject = Serializer.Load<LevelDataSaveObject>(Filepath);

        worldTiles = new TilemapWithInfo(saveObject.tilemapWithInfoSaveObject);

        gardens = new List<Garden>();

        foreach (GardenSaveObject gso in saveObject.gardens)
        {
            gardens.Add(new Garden(gso));
        }
    }
}
