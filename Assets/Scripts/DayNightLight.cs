using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLight : MonoBehaviour
{
    public Light sun;
    public float dayIntervalSec = 60f;
    public int tempDay = 6500;
    public int tempNight = 20000;

    private float sinPeriod;

    // Start is called before the first frame update
    void Start()
    {
        sinPeriod = 2.0f * Mathf.PI / dayIntervalSec;
    }

    // Update is called once per frame
    void Update()
    {
        float amplitude = (Mathf.Cos(sinPeriod * Time.time) + 1.0f) / 2.0f; // clamp 0 -> 1
        float temp = (tempNight - tempDay) * 1.0f * amplitude + tempDay;
        sun.colorTemperature = temp;
    }
}
