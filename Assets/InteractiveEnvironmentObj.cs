using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEnvironmentObj : MonoBehaviour
{
    public float taskTime;
    private float currentProgress;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void InteractionComplete()
    {
        // TODO make real events to happen on complete
        Destroy(gameObject);
    }
}
