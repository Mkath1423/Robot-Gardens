using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Garden : TilemapWithInfo
{
    public List<Vector3Int> validTiles = new List<Vector3Int>();

    public List<Vector3Int> validWalls = new List<Vector3Int>();

    public string name = "";

    public string id = "";

    public Sprite sprite;

    public Garden(GardenSaveObject saveObject) : base(saveObject)
    {
        layers = new TilemapWithInfoLayer[saveObject.layers.Count];

        foreach (TilemapWithInfoLayerSaveObject layerso in saveObject.layers)
        {
            layers[layerso.layerIndex] = new TilemapWithInfoLayer(layerso);
        }

        validTiles = saveObject.validTiles;
        validWalls = saveObject.validWalls;

        name = saveObject.name;
        id = saveObject.id;
        sprite = saveObject.sprite;

        OnAfterLoad();
    }

    // Tile Manipulations

    public override void SetTileInfo(Vector3Int position, InfoContainer value)
    {
        if (IsTileValid(position)) base.SetTileInfo(position, value);
        else if (IsWallValid(position)) base.SetTileInfo(position, value);
    }

    public override void RemoveTile(Vector3Int position, int layer)
    {
        if (IsTileValid(position)) base.RemoveTile(position, layer);
        else if (IsWallValid(position)) base.RemoveTile(position, layer);
    }

    // Checks
    public bool IsWallValid(Vector3Int wall)
    {
        foreach(Vector3Int validWall in validWalls)
        {
            if(wall.x == validWall.x && wall.y == validWall.y)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsTileValid(Vector3Int tile)
    {
        foreach(Vector3Int validTile in validTiles)
        {
            if(tile.x == validTile.x && tile.y == validTile.y)
            {
                return true;
            }
        }

        return false;
    }
}
