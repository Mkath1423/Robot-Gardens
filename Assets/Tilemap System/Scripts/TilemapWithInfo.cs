using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapWithInfo
{
    [SerializeField]
    public List<Vector3Int> positions = new List<Vector3Int>();

    [SerializeField]
    public List<InfoContainer> info = new List<InfoContainer>();

    public Dictionary<Vector3Int, InfoContainer> tileInfo = new Dictionary<Vector3Int, InfoContainer>();

    public TilemapWithInfo()
    {
        positions = new List<Vector3Int>();
        info = new List<InfoContainer>();
        tileInfo = new Dictionary<Vector3Int, InfoContainer>();
    }

    public TilemapWithInfo(TilemapWithInfoSaveObject saveObject)
    {
        positions = saveObject.positions;
        info = saveObject.info;

        OnAfterLoad();
    }

    public InfoContainer GetTileInfo(Vector3Int key)
    {
        return tileInfo[key];
    }

    public void SetTileInfo(Vector3Int key, InfoContainer value)
    {
        tileInfo[key] = value;

        Debug.Log($"{tileInfo[key].tileId} was added at position {key}");
    }

    public void RemoveTile(Vector3Int key)
    {
        tileInfo.Remove(key);
    }

    public void ClearTilemap()
    {
        Debug.Log("Clearing tilemap info");
        tileInfo.Clear();

    }

    public void OnBeforeSave()
    {
        positions.Clear();
        info.Clear();

        foreach (KeyValuePair<Vector3Int, InfoContainer> kvp in tileInfo)
        {
            positions.Add(kvp.Key);
            info.Add(kvp.Value);
        }

    }

    public void OnAfterLoad()
    {
        for (int i = 0; i != Mathf.Min(positions.Count, info.Count); i++)
        {
            tileInfo.Add(positions[i], info[i]);
        }

    }
}
