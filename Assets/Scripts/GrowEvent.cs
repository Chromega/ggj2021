using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowEvent : MonoBehaviour
{
    public GameObject[] growthProcessSteps;
    private int currStep = 0;
    private GameObject currentProcessStep;

    // Start is called before the first frame update
    void Start()
    {
        currentProcessStep = Instantiate(growthProcessSteps[currStep], gameObject.transform);
        GetComponent<InteractiveEnvironmentObj>().onInteractionComplete += Grow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Grow()
    {
        print("GROWING " + currStep);
        if(currStep < growthProcessSteps.Length - 1)
        {
            currStep++;
            UpdateCurrentAppearance();
        }
        else
        {
            GetComponent<InteractiveEnvironmentObj>().StopInteraction();
            // The process is complete, it's no longer interactive
            Destroy(GetComponent<InteractiveEnvironmentObj>());
        }
    }

    void UpdateCurrentAppearance()
    {
        Destroy(currentProcessStep);
        currentProcessStep = Instantiate(growthProcessSteps[currStep], gameObject.transform);
    }
}
