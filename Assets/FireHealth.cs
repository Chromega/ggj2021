using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public float currentHealth = 0;
    public float maxHealth = 25;
    public float maxEatDistance = 1.0f;
    public ParticleSystem flameParticles;
    public float minForFlames = 5.0f;
    public ParticleSystem smokeParticles;
    public float minForSmoke = 1.0f;
    private AudioSource mAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        mAudioSource = gameObject.GetComponent<AudioSource>();
        mAudioSource.volume = 0;
        flameParticles.gameObject.SetActive(false);
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
            
        if (currentHealth > minForFlames)
        {
            float targetedMax = 1.2f;
            float targetedMin = 0.7f;
            float targetedVolume = 1f;

            float currMin = targetedMin * (currentHealth / maxHealth);
            float currMax = targetedMax * (currentHealth / maxHealth);
            float currVol = targetedVolume * (currentHealth / maxHealth);

            main.startLifetime = new ParticleSystem.MinMaxCurve(currMin, currMax);
            mAudioSource.volume = currVol;
            flameParticles.gameObject.SetActive(true);
        }
        else
        {
            main.startLifetime = 0;
        }
    }

    void UpdateSmoke()
    {
        var main = smokeParticles.main;

        if (currentHealth > minForSmoke)
        {
            float targetedMax = 3.0f;

            float currMax = targetedMax * (currentHealth / maxHealth);

            main.startSize = currMax;
        }
        else
        {
            main.startSize = 0.1f;
        }
    }

}
