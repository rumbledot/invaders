using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffcultyClass
{
    private float healthFactorial;
    private float speedFactorial;
    private float timerFactorial;

    public DiffcultyClass(float h, float s, float t)
    {
        this.healthFactorial = h;
        this.speedFactorial = s;
        this.timerFactorial = t;
    }

    public float getHealthFactorial() { return healthFactorial; }
    public void setHealthFactorial(float h) { healthFactorial = h; }
    public float getSpeedFactorial() { return speedFactorial; }
    public void setSpeedFactorial(float s) { speedFactorial = s; }
    public float getTimerFactorial() { return timerFactorial; }
    public void setTimerFactorial(float t) { timerFactorial = t; }
}
