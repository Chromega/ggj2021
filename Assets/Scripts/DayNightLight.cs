using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightLight : MonoBehaviour
{
    public Light sun;
    public float dayIntervalSec = 60f;
    public int tempDay = 6500;
    public int tempNight = 20000;
    public float yDay = -90.0f;
    public float yNight = 90.0f;
    public int intensityDay = 120000;
    public int intensityNight = 1000;

    private HDAdditionalLightData hdLight;
    private float sinPeriod;

    // Start is called before the first frame update
    void Start()
    {
        sinPeriod = 2.0f * Mathf.PI / dayIntervalSec;
        hdLight = sun.GetComponent<HDAdditionalLightData>();
    }

    // Update is called once per frame
    void Update()
    {
        float amplitude = (Mathf.Cos(sinPeriod * Time.time) + 1.0f) / 2.0f; // clamp 0 -> 1
        float temp = (tempNight - tempDay) * 1.0f * amplitude + tempDay;
        sun.colorTemperature = temp;

        float intensity = (intensityNight - intensityDay) * 1.0f * amplitude + intensityDay;
        hdLight.intensity = intensity;

        float yRotation = (yNight - yDay) * 1.0f * amplitude + yDay;
        sun.transform.rotation = Quaternion.Euler(sun.transform.rotation.eulerAngles.x, yRotation, sun.transform.rotation.eulerAngles.z);
        // new Vector3(sun.transform.rotation.x, yRotation, sun.transform.rotation.z);
    }
}
