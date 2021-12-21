using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public string filename;
    public string Filepath { get { return Application.dataPath + "\\" + filename + ".txt"; } }

    public TilemapWithInfo worldTiles;

    public List<Garden> gardens;

    public TilemapWithInfoSaveObject defaultSaveObject = default;
    public GardenSaveObject defaultGardenSaveObject = default;

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
            worldTiles = new TilemapWithInfo(defaultSaveObject);
            gardens = new List<Garden>();
        }
    }

    public Garden GetGarden(string id)
    {
        foreach(Garden garden in gardens)
        {
            if (garden.id == id) return garden;
        }

        return null;
        // throw new KeyNotFoundException($"Id {id} was not found in gardens");
    }

    private void OnApplicationQuit()
    {

        foreach(TilemapWithInfoLayer layer in worldTiles.layers)
        {
            foreach(KeyValuePair<Vector3Int, InfoContainer> kvp in layer.tileInfo)
            {
                Debug.Log($"{kvp.Key} {kvp.Value}");
            }
        }
        Save();
    }

    private void Save()
    {
        // Serialize
        SaveObject saveObject = new LevelDataSaveObject()
        {
            tilemapWithInfoSaveObject = new TilemapWithInfoSaveObject(worldTiles)
        };

        Debug.Log(saveObject);
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
