using System;
using UnityEngine;

public class ComponentParameter
{
    [SerializeField]
    protected bool m_OverrideState;
    public static bool IsObjectParameter(Type type)
    {
        if (type.IsGenericType)
            return true;

        return type.BaseType != null
            && IsObjectParameter(type.BaseType);
    }
    public virtual bool overrideState
    {
        get => m_OverrideState;
        set => m_OverrideState = value;
    }
    protected internal virtual void OnEnable()
    {
    }

    protected internal virtual void OnDisable()
    {
    }
}
