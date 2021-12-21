using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapWithInfo
{
    public TilemapWithInfoLayer[] layers;

    public TilemapWithInfo(TilemapWithInfoSaveObject saveObject)
    {
        layers = new TilemapWithInfoLayer[saveObject.layers.Count];

        foreach (TilemapWithInfoLayerSaveObject layerso in saveObject.layers)
        {
            layers[layerso.layerIndex] = new TilemapWithInfoLayer(layerso);
        }
    }

    public virtual InfoContainer GetTileInfo(Vector3Int position, int layer)
    {
        return GetLayerByIndex(layer).tileInfo[position];
    }

    public virtual void SetTileInfo(Vector3Int position, InfoContainer value)
    {
        GetLayerByName(value.targetLayer).tileInfo[position] = value;
    }

    public virtual void RemoveTile(Vector3Int position, int layer)
    {
        GetLayerByIndex(layer).tileInfo.Remove(position);
    }

    public virtual void ClearTilemap(int layer)
    {
        GetLayerByIndex(layer).tileInfo.Clear();
    }

    public TilemapWithInfoLayer GetLayerByIndex(int index)
    {
        foreach(TilemapWithInfoLayer layer in layers)
        {
            if(layer.layerIndex == index)
            {
                return layer;
            }
        }

        return null;
    }

    public TilemapWithInfoLayer GetLayerByName(string name)
    {
        foreach (TilemapWithInfoLayer layer in layers)
        {
            if (layer.layerName == name)
            {
                return layer;
            }
        }

        return null;
    }

    public virtual void OnBeforeSave() { }
    public virtual void OnAfterLoad() { }
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


