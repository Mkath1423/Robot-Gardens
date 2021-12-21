using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newTileDatabase", menuName = "Databases/TileDatabase")]
public class TileBaseDatabase : ScriptableObject
{
    public List<string> keys = new List<string>();
    public List<TileWithInfo> values = new List<TileWithInfo>();

    public TileWithInfo GetTileBase(string key, TilemapWithInfo worldInfo)
    {
        int index = keys.IndexOf(key);

        if(index >= 0)
        {
            TileWithInfo output = values[index];

            output.tilemap = worldInfo;

            return output;
        }

        return null;
    }
}