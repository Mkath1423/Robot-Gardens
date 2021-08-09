using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Garden
{
    [SerializeField]
    public List<Vector3Int> positions = new List<Vector3Int>();

    [SerializeField]
    public List<InfoContainer> info = new List<InfoContainer>();
    
    public Dictionary<Vector3Int, InfoContainer> tileInfo = new Dictionary<Vector3Int, InfoContainer>();

    public List<Vector3Int> validTiles;

    public List<Vector3Int> validWalls;

    public string name;

    public string id;

    public Sprite sprite;

    // Constructors
    public Garden(List<Vector3Int> validTiles, List<Vector3Int> validWalls)
	{
        this.validTiles = validTiles;
        this.validWalls = validWalls;
    }

    public Garden(GardenSaveObject saveObject)
    {
        positions = saveObject.positions;
        info = saveObject.info;

        validTiles = saveObject.validTiles;
        validWalls = saveObject.validWalls;

        name = saveObject.name;
        id = saveObject.id;
        sprite = saveObject.sprite;

        OnAfterLoad();
    }

    // Tile Manipulations
    public bool SetTileInfo(Vector3Int position, InfoContainer info)
    {
        // check for valid placement
        if (IsTileValid(position)) return false;
        
        // place tile
        tileInfo[position] = info;
        return true;
    }

    public void RemoveTile(Vector3Int key)
    {
        tileInfo.Remove(key);
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

    //Saving and Loading
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
