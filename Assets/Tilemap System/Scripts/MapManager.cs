using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap map;

    public TileBaseDatabase tileBaseDatabase;
    public TileWithInfo tileToPlace;

    public LevelData level;
    public WorldInfo worldInfo;

    void Start()
    {
        SetLevelTiles(level, map);
        worldInfo = level.worldInfo;
    }

    void SetLevelTiles(LevelData level, Tilemap map)
    {
        map.ClearAllTiles();

        foreach (KeyValuePair<Vector3Int, InfoContainer> kvp in level.worldInfo.worldInfo)
        {
            map.SetTile(kvp.Key, tileBaseDatabase.GetTileBase(kvp.Value.tileId, level.worldInfo));
        }
    }

    public void ClearWorld()
    {
        map.ClearAllTiles();
        level.worldInfo.ClearWorld();
    }

    public void PlaceTile(Vector3Int position, TileWithInfo tileToPlace)
    {
        worldInfo.SetTileInfo(position, tileToPlace.GenerateTileInfoContainer());

        map.SetTile(position, tileToPlace);
    }

    public void RemoveTile(Vector3Int position)
    {
        level.worldInfo.RemoveTile(position);
        map.SetTile(position, null);
    }
}
