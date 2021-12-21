using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[CreateAssetMenu(fileName ="Ground Tile", menuName = "Tiles")]
public class GroundTile : TileWithInfo
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>(16);

    private int[][] positions = new int[][] {
                            new int[] { 0,  1 },
                            new int[] { 1,  0 },
                            new int[] { 0, -1 },
                            new int[] { -1, 0 } };

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
        
        foreach(int[] relativePosition in positions)
        {
            Vector3Int location = new Vector3Int(position.x + relativePosition[0], position.y + relativePosition[1], position.z);
            if(isConnected(location, tilemap))
            {
                tilemap.RefreshTile(location);
            }
        }

    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.colliderType = Tile.ColliderType.None;

        int[] values = new int[positions.Length];

        
        for(int i = 0; i < 4; i ++)
        {
            Vector3Int location = new Vector3Int(position.x + positions[i][0], position.y + positions[i][1], position.z);
            values[i] = isConnected(location, tilemap) ? 1 : 0;

            int tempVal = isConnected(location, tilemap) ? 1 : 0;

            //Debug.Log("Index: " + i + "Location: " + location.ToString() + "Result: " + tempVal);
        }

        int spriteToUse = 0;

        for(int i = 0; i < values.Length; i ++)
        {
            spriteToUse += values[i] * (int)Math.Pow(2, i);
            //Debug.Log($"{spriteToUse} {values[i] * 2 ^ i} {i}");
        }

        tileData.sprite = sprites[spriteToUse];
        //Debug.Log("Tile at " + position + " used sprite number " + spriteToUse);
        //Debug.Log("     1: " + values[0]);
        //Debug.Log("     2: " + values[1]);
        //Debug.Log("     3: " + values[2]);
        //Debug.Log("     4: " + values[3]);

        //tileData.sprite = sprites[0];
    }

    private bool isConnected(Vector3Int position, ITilemap tilemap)
    {
        TileBase tile = tilemap.GetTile(position);
        return (tile != null && tile == this);
    }

    public override InfoContainer GenerateTileInfoContainer()
    {
        return new InfoContainer(tileId, targetLayer);
    }

    public override void OnTileIsPlaced() { }
}
