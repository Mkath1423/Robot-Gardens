using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfoContainer
{
    public string tileId;
    public string targetLayer;

    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();

    public string GetValue (string key)
    {
        if (!keys.Contains(key)) return null;
        return values[keys.IndexOf(key)];
    }

    public InfoContainer(string _tileId, string _targetLayer, List<string> _keys = null, List<string> _values = null)
    {
        tileId = _tileId;
        keys = _keys;
        values = _values;
        targetLayer = _targetLayer;
    }

    public InfoContainer(InfoContainer toClone) 
    {
        tileId = toClone.tileId;
        keys = toClone.keys;
        values = toClone.values;
    }

}
/*
[Serializable]
public class PlantInfoContainer : InfoContainer
{
    public int growthStage;

    public override void Save()
    {
        Debug.Log("plant is saving");
        keys.Add("growthStage");
        values.Add("" + growthStage);
    }

    public override void Load()
    {
        if(!int.TryParse(values[keys.IndexOf("growthStage")], out growthStage))
        {
            Debug.Log("Growth stage data corrupted");
        }
    }

    public override InfoContainer Clone()
    {
        return new PlantInfoContainer() { growthStage = growthStage};
    }
    
}
*/