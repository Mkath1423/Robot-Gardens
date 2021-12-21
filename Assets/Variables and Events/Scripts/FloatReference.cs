using System;

public abstract class Reference
{
    public abstract void SetDefault();
}

[Serializable]
public class FloatReference : Reference
{
    public bool UseConstant;
    public float constantValue;
    public FloatValue floatValue;

    public float Value
    {
        get { return UseConstant ? constantValue : floatValue.value;  }
        set
        {
            if (UseConstant) constantValue = value;
            else floatValue.value = value;
        }
    }

    public override void SetDefault()
    {
        floatValue.SetDefault();
    }
}

[Serializable]
public class StringReference : Reference
{

    public bool UseConstant;
    public string constantValue;
    public StringValue stringValue;

    public string Value
    {
        get { return UseConstant ? constantValue : stringValue.value; }
        set
        {
            if (UseConstant) constantValue = value;
            else stringValue.value = value;
        }
    }

    public override void SetDefault()
    {
        stringValue.SetDefault();
    }
}

[Serializable]
public class BoolReference : Reference
{
    public bool UseConstant;
    public bool constantValue;
    public BoolValue boolValue;

    public bool Value
    {
        get { return UseConstant ? constantValue : boolValue.value; }
        set
        {
            if (UseConstant) constantValue = value;
            else boolValue.value = value;
        }
    }

    public override void SetDefault()
    {
        boolValue.SetDefault();
    }
}
