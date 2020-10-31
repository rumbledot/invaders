using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffcultyClass
{
    public float healthFactorial;
    public float speedFactorial;
    public float timerFactorial;

    public DiffcultyClass(float h, float s, float t)
    {
        this.healthFactorial = h;
        this.speedFactorial = s;
        this.timerFactorial = t;
    }

    public string HealthFactorial { get; }
    public string SpeedFactorial { get; }
    public string TimerFactorial { get; }
}
