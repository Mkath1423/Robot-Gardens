using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldInfo
{
    [SerializeField]
    public List<Vector3Int> positions = new List<Vector3Int>();

    [SerializeField]
    public List<InfoContainer> info = new List<InfoContainer>();

    public Dictionary<Vector3Int, InfoContainer> worldInfo = new Dictionary<Vector3Int, InfoContainer>();

    public WorldInfo()
    {
        positions = new List<Vector3Int>();
        info = new List<InfoContainer>();
        worldInfo = new Dictionary<Vector3Int, InfoContainer>();
    }

    public WorldInfo(WorldInfoSaveObject saveObject)
    {
        positions = saveObject.positions;
        info = saveObject.info;

        OnAfterLoad();
    }

    public InfoContainer GetTileInfo(Vector3Int key)
    {
        return worldInfo[key];
    }

    public void SetTileInfo(Vector3Int key, InfoContainer value)
    {
        worldInfo[key] = value;

        Debug.Log($"{worldInfo[key].tileId} was added at position {key}");
    }

    public void RemoveTile(Vector3Int key)
    {
        worldInfo.Remove(key);
    }

    public void ClearWorld()
    {
        Debug.Log("Clearing world info");
        worldInfo.Clear();

    }

    public void OnBeforeSave()
    {
        positions.Clear();
        info.Clear();

        foreach (KeyValuePair<Vector3Int, InfoContainer> kvp in worldInfo)
        {
            positions.Add(kvp.Key);
            info.Add(kvp.Value);
        }

    }

    public void OnAfterLoad()
    {
        for (int i = 0; i != Mathf.Min(positions.Count, info.Count); i++)
        {
            worldInfo.Add(positions[i], info[i]);
        }

    }
    
}
