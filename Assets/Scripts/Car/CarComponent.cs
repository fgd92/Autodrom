using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

[Serializable]
public class CarComponent : ScriptableObject
{
    public string displayName { get; protected set; } = "";
    public List<ComponentParameter> parameters { get; private set; }

    protected virtual void OnEnable()
    {
        parameters = GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(t => t.FieldType.IsSubclassOf(typeof(ComponentParameter)))
            .OrderBy(t => t.MetadataToken)
            .Select(t => (ComponentParameter)t.GetValue(this))
            .ToList();

        foreach (var parameter in parameters)
        {
            if(parameter != null)
                parameter.OnEnable();
        }
    }
}
