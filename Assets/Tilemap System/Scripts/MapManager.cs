using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public List<Tilemap> worldTilemaps;
    public List<Tilemap> gardenTilemaps;

    public TileBaseDatabase tileBaseDatabase;
    public TileWithInfo tileToPlace;

    public LevelData level;
    public TilemapWithInfo worldTiles;

    void Start()
    {
        worldTiles = level.worldTiles;

        SetLevelTiles(worldTiles, worldTilemaps);
    }

    void SetLevelTiles(TilemapWithInfo tiles, List<Tilemap> maps)
    {
        foreach (Tilemap map in maps)
        {
            map.ClearAllTiles();
        }

        foreach (KeyValuePair<Vector3Int, InfoContainer> kvp in tiles.tileInfo)
        {
            maps[kvp.Key.z].SetTile(kvp.Key, tileBaseDatabase.GetTileBase(kvp.Value.tileId, tiles));
        }
    }

    public void ClearWorld(Tilemap map)
    {
        map.ClearAllTiles();
        level.worldTiles.ClearTilemap();
    }

    public void PlaceTile(Vector3Int position, TileWithInfo tileToPlace, Tilemap map)
    {
        worldTiles.SetTileInfo(position, tileToPlace.GenerateTileInfoContainer());

        map.SetTile(position, tileToPlace);
    }

    public void RemoveTile(Vector3Int position, Tilemap map)
    {
        level.worldTiles.RemoveTile(position);
        map.SetTile(position, null);
    }
}
