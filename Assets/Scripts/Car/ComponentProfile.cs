using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentProfile : ScriptableObject
{
    public List<CarComponent> componentCars = new List<CarComponent>();

    public bool isDirty = true; 

    void OnEnable()
    {
        componentCars.RemoveAll(x => x == null);
    }
    public void Reset()
    {
        isDirty = true;
    }
    public T Add<T>(bool overrides = false)
           where T : CarComponent
    {
        return (T)Add(typeof(T), overrides);
    }
    public CarComponent Add(Type type, bool overrides = false)
    {
        if (Has(type))
            throw new InvalidOperationException("Component already exists in the car");

        var component = (CarComponent)CreateInstance(type);
        componentCars.Add(component);
        isDirty = true;
        return component;
    }
    public void Remove<T>()
         where T : CarComponent
    {
        Remove(typeof(T));
    }
    public void Remove(Type type)
    {
        int toRemove = -1;

        for (int i = 0; i < componentCars.Count; i++)
        {
            if (componentCars[i].GetType() == type)
            {
                toRemove = i;
                break;
            }
        }

        if (toRemove >= 0)
        {
            componentCars.RemoveAt(toRemove);
            isDirty = true;
        }
    }
    public bool Has<T>()
      where T : CarComponent
    {
        return Has(typeof(T));
    }
    public bool Has(Type type)
    {
        foreach (var component in componentCars)
        {
            if (component.GetType() == type)
                return true;
        }

        return false;
    }
}
