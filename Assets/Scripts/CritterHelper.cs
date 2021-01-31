using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterHelper : MonoBehaviour
{
    public float speed = 2f;
    public float workTime = 10f;
    private float currWorkShift = 10f;
    public float restTime = 10f;
    private float currRestBreak = 0f;

    enum ActivityState { working, resting }
    ActivityState currState = ActivityState.working;

    GameObject closestObject;
    EnvironmentInteractor myInteractor;

    Animator animator;

    float angle = 0.0f;

    void Start()
    {
        StartCoroutine(ChangeDirection());
        myInteractor = GetComponent<EnvironmentInteractor>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currState == ActivityState.working)
        {
            Work();
        }
        else
        {
            Rest();
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            angle = Random.Range(0, 2 * Mathf.PI);

            float timeToWait = Random.Range(4.0f, 6.0f);

            yield return new WaitForSeconds(timeToWait);
        }
    }


    void Work()
    {
        currWorkShift -= Time.deltaTime;

        if (!closestObject)
        {
            closestObject = myInteractor.FindClosestObjectWithTag("interactiveEnvironment", false);
        }
        Vector3 relativePos = closestObject.transform.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(relativePos, Vector3.up),
            Time.deltaTime * 30f);

        if (myInteractor.minimumDistance < Vector3.Distance(transform.position, closestObject.transform.position))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (animator)
                animator.SetBool("interacting", false);
            if (animator)
                animator.SetBool("walking", true);
        }
        else
        {
            myInteractor.InteractWithNearbySurroudings();
            if (!myInteractor.interactionInProgress)
            {
                closestObject = null;
                if (animator)
                    animator.SetBool("interacting", false);
            }
            else
            {
                if (animator)
                    animator.SetBool("interacting", true);
            }
            //if (animator)
            //    animator.SetBool("walking", false);
        }

        if(currWorkShift <= 0)
        {
            myInteractor.CancelInteraction();
            if (animator)
                animator.SetBool("interacting", false);
            currRestBreak = restTime;
            currState = ActivityState.resting;
            closestObject = null;
        }
    }

    void Rest()
    {
        currRestBreak -= Time.deltaTime;

        GameObject fireplace = GameObject.FindGameObjectWithTag("campfire");

        Vector3 relativePos = fireplace.transform.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(relativePos, Vector3.up),
            Time.deltaTime * 30f);

        if (myInteractor.minimumDistance < Vector3.Distance(transform.position, fireplace.transform.position))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (animator)
                animator.SetBool("walking", true);
        }
        else
        {
            if (animator)
                animator.SetBool("walking", false);
        }

        if (currRestBreak <= 0)
        {
            currWorkShift = workTime;
            currState = ActivityState.working;
        }
    }
}
