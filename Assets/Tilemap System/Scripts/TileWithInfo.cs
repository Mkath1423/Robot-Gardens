using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileWithInfo : TileBase
{
    public WorldInfo worldInfo;
    public string tileId;
    public abstract override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData);
    public abstract override void RefreshTile(Vector3Int position, ITilemap tilemap);
    public abstract InfoContainer GenerateTileInfoContainer();

}
