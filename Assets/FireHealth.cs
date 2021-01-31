using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public float currentHealth = 0;
    private float lastHealth = 0;
    public float maxHealth = 25;
    public float maxEatDistance = 1.0f;
    public ParticleSystem flameParticles;
    public float minForFlames = 5.0f;
    public ParticleSystem smokeParticles;
    public float minForSmoke = 1.0f;
    public float maxSmokeSize = 3.0f;
    private AudioSource mAudioSource;

    public float flareTime = 1.0f;
    private float currFlareLife = 0f;
    private bool flaring;

    public Light camplight;
    public float maxLightIntensity = 2000000f;
    private float currLightIntensity = 0f;


    // Start is called before the first frame update
    void Start()
    {
        mAudioSource = gameObject.GetComponent<AudioSource>();
        mAudioSource.volume = 0;
        flameParticles.gameObject.SetActive(false);
        camplight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFire();
        UpdateSmoke();
    }

    void UpdateFire()
    {
        var main = flameParticles.main;
        float targetedMax = 1.2f;
        float targetedMin = 0.7f;
        float targetedVolume = 0.75f;

        float currMin = targetedMin * (currentHealth / maxHealth);
        float currMax = targetedMax * (currentHealth / maxHealth);
        float currVol = targetedVolume * (currentHealth / maxHealth);
        currLightIntensity = maxLightIntensity * (currentHealth / maxHealth);

        if (currentHealth > minForFlames)
        {         
            main.startLifetime = new ParticleSystem.MinMaxCurve(currMin, currMax);
            mAudioSource.volume = currVol;
            flameParticles.gameObject.SetActive(true);
            camplight.intensity = currLightIntensity;
        }
        
        if(flaring)
        {
            print("FLARING IN PROGRESS " + currFlareLife);
            currFlareLife -= Time.deltaTime;
            //main.startLifetime = new ParticleSystem.MinMaxCurve(currFlareLife, currFlareLife * 2f);// new ParticleSystem.MinMaxCurve(currFlareLife, currFlareLife);
            main.startLifetime = currFlareLife;// new ParticleSystem.MinMaxCurve(currFlareLife, currFlareLife * 2f);
            flameParticles.gameObject.SetActive(true);
            mAudioSource.volume = currVol + currFlareLife;

            if (currFlareLife <= 0)
            {
                flaring = false;
            }
        }
        else if (currentHealth < minForFlames)
        {
            main.startLifetime = 0;
            flameParticles.gameObject.SetActive(false);
            camplight.intensity = 0;
        }

        if (currentHealth != lastHealth)
        {
            print("FLARING");
            lastHealth = currentHealth;
            camplight.intensity = currLightIntensity * 1.25f;
            Flare();
        }
    }

    void UpdateSmoke()
    {
        var main = smokeParticles.main;

        if (currentHealth > minForSmoke)
        {
            float targetedMax = 3.0f;

            float currMax = targetedMax * (currentHealth / maxHealth);
            
            if(currMax > maxSmokeSize)
            {
                currMax = maxSmokeSize;
            }

            main.startSize = currMax;
        }
        else
        {
            main.startSize = 0.1f;
        }
        
    }

    void Flare()
    {
        var main = flameParticles.main;
        currFlareLife = flareTime;
        flaring = true;
        mAudioSource.volume = flareTime;
        flameParticles.gameObject.SetActive(true);
        main.startLifetime = new ParticleSystem.MinMaxCurve(currFlareLife, currFlareLife * 2f);
    }

}
