using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

[Serializable]
public class CarComponent : ScriptableObject, ICarComponent
{
    public bool active = true;
    public string displayName { get; protected set; } = "";
    public ReadOnlyCollection<ComponentParameter> parameters { get; private set; }

    protected virtual void OnEnable()
    {
        parameters = this.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(t => t.FieldType.IsSubclassOf(typeof(ComponentParameter)))
            .OrderBy(t => t.MetadataToken) 
            .Select(t => (ComponentParameter)t.GetValue(this))
            .ToList()
            .AsReadOnly();

        foreach (var parameter in parameters)
            parameter.OnEnable();
    }
}
