using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveObject { }

[System.Serializable]
public class TilemapWithInfoLayerSaveObject : SaveObject
{
    public int layerIndex;
    public string layerName;

    public List<Vector3Int> positions;
    public List<InfoContainer> info;

    public TilemapWithInfoLayerSaveObject(TilemapWithInfoLayer layer)
    {
        layer.OnBeforeSave();

        positions = layer.positions;
        info = layer.info;

        layerName = layer.layerName;
        layerIndex = layer.layerIndex;
    }
}

[System.Serializable]
public class TilemapWithInfoSaveObject : SaveObject
{
    public List<TilemapWithInfoLayerSaveObject> layers;

    public TilemapWithInfoSaveObject(TilemapWithInfo tilemap)
    {
        layers = new List<TilemapWithInfoLayerSaveObject>();

        foreach(TilemapWithInfoLayer layer in tilemap.layers)
        {
            layers.Add(new TilemapWithInfoLayerSaveObject(layer));
        }
    }
}

[System.Serializable]
public class GardenSaveObject : TilemapWithInfoSaveObject
{

    public List<Vector3Int> validTiles;
    public List<Vector3Int> validWalls;

    public string name;
    public string id;
    public Sprite sprite;

    public GardenSaveObject(Garden garden) : base(garden)
    {
        garden.OnBeforeSave();

        layers.Clear();

        foreach (TilemapWithInfoLayer layer in garden.layers)
        {
            layers.Add(new TilemapWithInfoLayerSaveObject(layer));
        }

        validTiles = garden.validTiles;
        validWalls = garden.validWalls;
        
        name = garden.name;
        id = garden.id;
        sprite = garden.sprite;
    }
}

[System.Serializable]
public class LevelDataSaveObject: SaveObject
{
    public TilemapWithInfoSaveObject tilemapWithInfoSaveObject;

    public List<GardenSaveObject> gardens;

}
