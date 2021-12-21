using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetValues : MonoBehaviour
{
    public List<FloatValue> floatValues;
    public List<BoolValue> boolValues;
    public List<StringValue> stringValues;

    void Start()
    {
        foreach(FloatValue value in floatValues)
        {
            value.SetDefault();
        }

        foreach (BoolValue value in boolValues)
        {
            value.SetDefault();
        }

        foreach (StringValue value in stringValues)
        {
            value.SetDefault();
        }
    }
}
