using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRateToGameState : MonoBehaviour
{
    public float minHealth = 20f;
    public float maxIntensity = 1.5f;
    ParticleSystem myParticleSystem;
    public float variance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        float currForestHealth = GameManager.Instance.forestHealth;
        myParticleSystem = gameObject.GetComponent<ParticleSystem>();
        var main = myParticleSystem.main;
        main.startLifetime = Random.Range((currForestHealth / 50f) - 1f, (currForestHealth / 50f) - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float currForestHealth = GameManager.Instance.forestHealth;
        if(currForestHealth > minHealth)
        {
            var main = myParticleSystem.main;
            main.startLifetime = Random.Range((currForestHealth / 50f) - 1f, (currForestHealth / 50f) - 0.5f);
        }
    }
}
