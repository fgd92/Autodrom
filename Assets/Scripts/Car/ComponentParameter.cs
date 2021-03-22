using System;
using UnityEngine;

public abstract class ComponentParameter
{

    public static bool IsObjectParameter(Type type)
    {
        if (type.IsGenericType)
            return true;

        return type.BaseType != null
            && IsObjectParameter(type.BaseType);
    }
    protected internal virtual void OnEnable()
    {

    }

    protected internal virtual void OnDisable()
    {
    }
    public T GetValue<T>()
    {
        return ((ComponentParameter<T>)this).Value;
    }

    public abstract void SetValue(ComponentParameter parameter);

}
public class ComponentParameter<T> : ComponentParameter
{
    protected T value;
    public virtual T Value
    {
        get => value;
        set => this.value = value;
    }
    protected ComponentParameter(T value)
    {
        this.value = value;
    }

    public override void SetValue(ComponentParameter parameter)
    {
        value = parameter.GetValue<T>();
    }
    public T GetValue()
    {
        return Value;
    }
}
public class FloatParameter : ComponentParameter<float>
{
    public FloatParameter(float value) : base(value)
    {
    }
    public override float Value { get => base.Value; set => base.Value = value; }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void SetValue(ComponentParameter parameter)
    {
        base.SetValue(parameter);
    }
}
public class BooleanParameter : ComponentParameter<bool>
{
    public BooleanParameter(bool value) : base(value)
    {
    }
    public override bool Value { get => base.Value; set => base.Value = value; }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void SetValue(ComponentParameter parameter)
    {
        base.SetValue(parameter);
    }
}