using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MultiTiele : TileWithInfo
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>(16);

    private Vector3Int[] positions = new Vector3Int[]
    {
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1,  0, 0),
        new Vector3Int(-1,  1, 0),
        new Vector3Int( 0,  1, 0),
        new Vector3Int( 0, -1, 0),
        new Vector3Int( 1, -1, 0),
        new Vector3Int( 1,  0, 0),
        new Vector3Int( 1,  1, 0)
    };



    public override InfoContainer GenerateTileInfoContainer()
    {
        throw new System.NotImplementedException();
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        throw new System.NotImplementedException();
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        throw new System.NotImplementedException();
    }

    public void isConnected(Vector3Int position, )
    {

    
    }
}
