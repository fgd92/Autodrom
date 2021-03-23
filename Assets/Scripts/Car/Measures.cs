using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="Meaures", menuName ="Measures")]
public class Measures
{
    public int Count 
    {
        get
        { 
            return Items.Count;
        }
    }
    public List<Measure> Items = new List<Measure>();
    public Measure this[TypeMeasure typeMeasure]
    {
        get
        {
            return FindByTypeMeasure(typeMeasure);
        }
        set
        {
            Measure measure = FindByTypeMeasure(typeMeasure);
            measure = measure ? measure : value;
        }
    }
    public Measure this[int index]
    {
        get
        {
            return Items[index];
        }
        set
        {
            Items[index]=  value;
        }
    }
    private Measure FindByTypeMeasure(TypeMeasure typeMeasure)
    {
        Measure measure = null;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].TypeMeasure == typeMeasure)
            {
                measure = Items[i];
            }
        }

        return measure;
    }

    public void Add(Measure measure)
    {
        Items.Add(measure);
    }

    public void Remove(Measure measure)
    {
        Items.Remove(measure);
    }

    public Measure Last()
    {
        return Items.Last();
    }
}