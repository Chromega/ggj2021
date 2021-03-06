using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveEnvironmentObj : MonoBehaviour
{
    public float taskTime;
    private float currentProgress;
    private ParticleSystem myParticleSystem;

    public System.Action onInteractionComplete;


    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptInteractionTime()
    {
        currentProgress += Time.deltaTime;
        if (myParticleSystem)
        {
            ParticleSystem.EmissionModule emitter = myParticleSystem.emission;
            emitter.enabled = true;
            if (!myParticleSystem.isPlaying)
                myParticleSystem.Play();
        }
        if (currentProgress >= taskTime)
        {
            InteractionComplete();
        }
    }

    public void StopInteraction()
    {
        if (myParticleSystem)
        {
            ParticleSystem.EmissionModule emitter = myParticleSystem.emission;
            emitter.enabled = false;
        }
    }

    private void InteractionComplete()
    {
        if (myParticleSystem)
        {
            ParticleSystem.EmissionModule emitter = myParticleSystem.emission;
            emitter.enabled = false;
        }

        if (onInteractionComplete != null)
        {
            onInteractionComplete();
        }
        ResetInteractionComponent();
    }

    private void ResetInteractionComponent()
    {
        currentProgress = 0;
    }

    private void OnDestroy()
    {
        gameObject.tag = "Untagged";
    }
}
