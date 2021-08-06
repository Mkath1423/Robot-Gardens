using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveObject { }

[System.Serializable]
public class WorldInfoSaveObject : SaveObject
{
    
    public List<Vector3Int> positions;
    public List<InfoContainer> info;

    public WorldInfoSaveObject(WorldInfo worldInfo)
    {
        worldInfo.OnBeforeSave();

        positions = worldInfo.positions;
        info = worldInfo.info;
    }
}

public class LevelDataSaveObject: SaveObject
{
    public WorldInfoSaveObject worldInfoSaveObject;
}
