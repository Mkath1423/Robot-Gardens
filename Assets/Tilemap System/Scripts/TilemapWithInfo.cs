using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TilemapWithInfo
{
    public TilemapWithInfoLayer[] layers;

    public int amountOfLayers = 3;

    public virtual InfoContainer GetTileInfo(Vector3Int position)
    {
        return layers[position.z].tileInfo[position];
    }

    public virtual void SetTileInfo(Vector3Int position, InfoContainer value)
    {
        layers[position.z].tileInfo[position] = value;
    }

    public virtual void RemoveTile(Vector3Int position)
    {
        layers[position.z].tileInfo.Remove(position);
    }

    public virtual void ClearTilemap(int layer)
    {
        layers[layer].tileInfo.Clear();
    }
}

public enum LayerOrder
{
    Ground =0,
    Roads = 1,
    Machines=2,
    Decoration=3
}

public class TilemapWithInfoLayer
{
    public string layerName;

    public int layerIndex;

    [SerializeField]
    public List<Vector3Int> positions = new List<Vector3Int>();

    [SerializeField]
    public List<InfoContainer> info = new List<InfoContainer>();

    public Dictionary<Vector3Int, InfoContainer> tileInfo = new Dictionary<Vector3Int, InfoContainer>();

    public TilemapWithInfoLayer(string layerName, int layerIndex)
    {
        positions = new List<Vector3Int>();
        info = new List<InfoContainer>();

        tileInfo = new Dictionary<Vector3Int, InfoContainer>();

        this.layerName = layerName;
        this.layerIndex = layerIndex;
    }

    public TilemapWithInfoLayer(TilemapWithInfoLayerSaveObject saveObject)
    {
        positions = saveObject.positions;
        info = saveObject.info;

        layerName = saveObject.layerName;
        layerIndex = saveObject.layerIndex;

        OnAfterLoad();
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

/*
public virtual void OnBeforeSave()
{
    foreach(TilemapWithInfoLayer layer in layers)
    {
        layer.OnBeforeSave();
    }
}

public virtual void OnAfterLoad()
{
    foreach (TilemapWithInfoLayer layer in layers)
    {
        layer.OnAfterLoad();
    }
}

public TilemapWithInfo()
{
    layers.Clear();

    for(int i = 0; i != amountOfLayers; i++)
    {
        layers.Add(new TilemapWithInfoLayer("", i));
    }
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
    Debug.Log("Clearing tilemnap info");
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

*/


