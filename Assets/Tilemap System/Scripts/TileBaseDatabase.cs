using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newTileDatabase", menuName = "Databases/TileDatabase")]
public class TileBaseDatabase : ScriptableObject
{
    public List<string> keys = new List<string>();
    public List<TileWithInfo> values = new List<TileWithInfo>();

    public TileWithInfo GetTileBase(string key, WorldInfo worldInfo)
    {
        // Init values here
        TileWithInfo output = values[keys.IndexOf(key)];
        output.worldInfo = worldInfo;
        return output;
    }

}
