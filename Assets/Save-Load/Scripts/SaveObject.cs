using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveObject { }

[System.Serializable]
public class TilemapWithInfoSaveObject : SaveObject
{ 
    public List<Vector3Int> positions;
    public List<InfoContainer> info;

    public TilemapWithInfoSaveObject(TilemapWithInfo tiles)
    {
        tiles.OnBeforeSave();

        positions = tiles.positions;
        info = tiles.info;
    }
}

public class GardenSaveObject : SaveObject
{
    public List<Vector3Int> positions;
    public List<InfoContainer> info;

    public List<Vector3Int> validTiles;
    public List<Vector3Int> validWalls;

    public string name;
    public string id;
    public Sprite sprite;

    public GardenSaveObject(Garden garden)
    {
        garden.OnBeforeSave();

        positions = garden.positions;
        info = garden.info;

        validTiles = garden.validTiles;
        validWalls = garden.validWalls;
        
        name = garden.name;
        id = garden.id;
        sprite = garden.sprite;
    }
}

public class LevelDataSaveObject: SaveObject
{
    public TilemapWithInfoSaveObject tilemapWithInfoSaveObject;

    public List<GardenSaveObject> gardens;

}
