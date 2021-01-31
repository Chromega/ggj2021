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
        if(currentProgress >= taskTime)
        {
            InteractionComplete();
        }
        if (myParticleSystem && !myParticleSystem.IsAlive())
        {
            myParticleSystem.Emit(10);
        }
    }

    private void InteractionComplete()
    {
        // TODO make real events to happen on complete
        if (myParticleSystem)
        {
            myParticleSystem.Emit(25); // Doesn't actually work, it dies too soon.
        }

        if(onInteractionComplete != null)
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
