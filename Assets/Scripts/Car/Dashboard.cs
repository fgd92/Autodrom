using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dashboard : MonoBehaviour, IDashboard
{
    public List<Measure> Measures;

    public void SetValueMeasure(TypeMeasure typeMeasure, float value)
    {
        Measure measure = Measures.FirstOrDefault(m => m.TypeMeasure == typeMeasure);

        if (measure != null)
        {
            measure.SetValue(value);
        }
    }
}
