using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentProfile : ScriptableObject
{
    public List<CarComponent> componentsCar = new List<CarComponent>();

    public CarComponent this[int index]
    {
        get
        {
            return componentsCar[index];
        }
        set 
        {
            componentsCar[index] = value;
        }
    }
    public bool isDirty = true; 

    void OnEnable()
    {
        componentsCar.RemoveAll(x => x == null);
    }
    public void Reset()
    {
        isDirty = true;
    }
    public int Count()
    {
        return componentsCar != null && componentsCar.Count > 0? componentsCar.Count : 0;
    }
    public CarComponent Get(int index)
    {
        return componentsCar[index];
    }
    public CarComponent Add(CarComponent carComponent)
    {
        var component = (CarComponent)CreateInstance(carComponent.GetType());
        componentsCar.Add(component);
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

        for (int i = 0; i < componentsCar.Count; i++)
        {
            if (componentsCar[i].GetType() == type)
            {
                toRemove = i;
                break;
            }
        }

        if (toRemove >= 0)
        {
            componentsCar.RemoveAt(toRemove);
            isDirty = true;
        }
    }
    public void RemoveLast()
    {
        if (Count() > 0)
        {
            componentsCar.RemoveAt(Count() - 1);
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
    public bool Has<T>()
      where T : CarComponent
    {
        return Has(typeof(T));
    }
    public bool Has(Type type)
    {
        foreach (var component in componentsCar)
        {
            if (component.GetType() == type)
                return true;
        }

        return false;
    }
}
