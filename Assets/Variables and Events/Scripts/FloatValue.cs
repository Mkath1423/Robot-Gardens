using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Value : ScriptableObject
{
    public abstract void SetDefault();
}

[CreateAssetMenu(fileName = "newFloatValue", menuName = "Variables/Float")]
public class FloatValue : Value
{
    public float value;
    public float default_value;

    public void SetValue(float f) { value = f; }
    public override void SetDefault() { value = default_value; }
    private void Awake() { SetDefault(); }
}

[CreateAssetMenu(fileName = "newStringValue", menuName = "Variables/String")]
public class StringValue : Value
{
    public string value;
    public string default_value;

    public void SetValue(string s) { value = s; }
    public override void SetDefault() { value = default_value; }
    private void Awake() { SetDefault(); }
}

[CreateAssetMenu(fileName = "newStringBool", menuName = "Variables/Bool")]
public class BoolValue : Value
{
    public bool value;
    public bool default_value;

    public void SetValue(bool b) { value = b; }
    public override void SetDefault() { value = default_value; }
    private void Awake() { SetDefault(); }
}
