using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteractor : MonoBehaviour
{
    public float minimumDistance = 2f;

    public bool interactionInProgress;
    private InteractiveEnvironmentObj currentInteractionGameObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InteractWithNearbySurroudings()
    {
        if (interactionInProgress)
        {
            if (currentInteractionGameObj != null)
            {
                float interactiveObjDistance = Vector3.Distance(transform.position, currentInteractionGameObj.transform.position);
                if (interactiveObjDistance < minimumDistance)
                {
                    currentInteractionGameObj.AcceptInteractionTime();
                }
                else
                {
                    CancelInteraction();
                }
            }
            else
            {
                CompleteInteraction();
            }
        }
        else
        {
            StartInteraction();
        }
    }

    void CompleteInteraction()
    {
        interactionInProgress = false;
        currentInteractionGameObj = null;
        GameManager.Instance.IncreaseForestHealth(1);
        print("INTERACTION COMPLETE!");
    }

    public void CancelInteraction()
    {
        interactionInProgress = false;
        currentInteractionGameObj = null;
        print("INTERACTION CANCELLED");
    }

    void StartInteraction()
    {

        GameObject newInteractiveGameObject = FindClosestObjectWithTag("interactiveEnvironment");
        if(Vector3.Distance(transform.position, newInteractiveGameObject.transform.position) < minimumDistance)
        {
            interactionInProgress = true;
            currentInteractionGameObj = newInteractiveGameObject.GetComponent<InteractiveEnvironmentObj>();
            print("INTERACTION STARTED");s
        }
    }

    public GameObject FindClosestObjectWithTag(string targetTag)
    {
        GameObject[] interactiveObjects = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closestObj = null;
        for(int i = 0; i < interactiveObjects.Length; i++)
        {
            if((!closestObj || Vector3.Distance(transform.position, interactiveObjects[i].transform.position) < Vector3.Distance(transform.position, closestObj.transform.position)) && interactiveObjects[i].GetComponent<InteractiveEnvironmentObj>())
            {
                closestObj = interactiveObjects[i];
            }
        }

        return closestObj;
    }
}
