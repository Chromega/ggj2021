using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEnvironmentObj : MonoBehaviour
{
    public float taskTime;
    private float currentProgress;
    private ParticleSystem myParticleSystem;


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
        if (!myParticleSystem.IsAlive())
        {
            myParticleSystem.Emit(10);
        }
    }

    private void InteractionComplete()
    {
        // TODO make real events to happen on complete
        myParticleSystem.Emit(25); // Doesn't actually work, it dies too soon. Nick: Think about how to fix
        Destroy(gameObject);
    }
}
