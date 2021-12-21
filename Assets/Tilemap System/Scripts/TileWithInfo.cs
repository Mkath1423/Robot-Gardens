using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileWithInfo : TileBase
{
    public TilemapWithInfo tilemap;
    public string tileId;
    public string targetLayer;
    
    public abstract override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData);
    public abstract override void RefreshTile(Vector3Int position, ITilemap tilemap);
    public abstract InfoContainer GenerateTileInfoContainer();
    public virtual void OnTileIsPlaced() { }

}
