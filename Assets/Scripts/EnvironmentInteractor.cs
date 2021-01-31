using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteractor : MonoBehaviour
{
    public float minimumDistance = 2f;

    public bool interactionInProgress;
    private InteractiveEnvironmentObj currentInteractionGameObj;
    public Transform sourceXfm;
    public bool needsToFaceObject = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!sourceXfm)
            sourceXfm = transform;
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
        if (currentInteractionGameObj)
            currentInteractionGameObj.StopInteraction();
        interactionInProgress = false;
        currentInteractionGameObj = null;
        GameManager.Instance.IncreaseForestHealth(1);
        //print("INTERACTION COMPLETE!");
    }

    public void CancelInteraction()
    {
        if (currentInteractionGameObj)
            currentInteractionGameObj.StopInteraction();
        interactionInProgress = false;
        currentInteractionGameObj = null;
        //print("INTERACTION CANCELLED");
    }

    void StartInteraction()
    {

        GameObject newInteractiveGameObject = FindClosestObjectWithTag("interactiveEnvironment", needsToFaceObject);
        if(Vector3.Distance(transform.position, newInteractiveGameObject.transform.position) < minimumDistance)
        {
            interactionInProgress = true;
            currentInteractionGameObj = newInteractiveGameObject.GetComponent<InteractiveEnvironmentObj>();
            //print("INTERACTION STARTED");
        }
    }

    public GameObject FindClosestObjectWithTag(string targetTag, bool checkFacing )
    {
        GameObject[] interactiveObjects = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closestObj = null;
        float myAngle = Mathf.Atan2(sourceXfm.right.z, sourceXfm.right.x);
        for (int i = 0; i < interactiveObjects.Length; i++)
        {
            Vector3 vecToObj = sourceXfm.position - interactiveObjects[i].transform.position;
            if ((!closestObj || Vector3.SqrMagnitude(vecToObj) < Vector3.SqrMagnitude(sourceXfm.position - closestObj.transform.position)) && interactiveObjects[i].GetComponent<InteractiveEnvironmentObj>())
            {
                if (checkFacing)
                {
                    Vector3 offsetSourcePos = sourceXfm.position + sourceXfm.right * 0.5f;
                    Vector3 offsetVecToObj = offsetSourcePos - interactiveObjects[i].transform.position;
                    float angle = Mathf.Atan2(offsetVecToObj.z, offsetVecToObj.x);

                    float deltaAngle = angle - myAngle;
                    while (deltaAngle < -Mathf.PI)
                    {
                        deltaAngle += 2*Mathf.PI;
                    }
                    while (deltaAngle > Mathf.PI)
                    {
                        deltaAngle -= 2*Mathf.PI;
                    }

                    if (Mathf.Abs(deltaAngle) > Mathf.PI/3) //toss out if not within 90 degree cone (-45,45)
                    {
                        continue;
                    }
                }
                closestObj = interactiveObjects[i];
            }
        }

        return closestObj;
    }

    public GameObject TakeNearbyObject()
    {
        GameObject newInteractiveGameObject = FindClosestObjectWithTag("interactiveEnvironment", needsToFaceObject);
        if (!newInteractiveGameObject) {
            Debug.Log("no interactive environment objects");
            return null;
        }

        if (Vector3.Distance(transform.position, newInteractiveGameObject.transform.position) < minimumDistance)
        {
            Debug.Log("Found nearby interactive environment object "+newInteractiveGameObject);
            return newInteractiveGameObject;
        }
        else
        {
            Debug.Log("Found a nearby interactive object, but it was too far away");
            return null;
        }
    }
}
