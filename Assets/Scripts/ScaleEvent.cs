using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEvent : MonoBehaviour
{
    public float targetMinScale = 1.0f;
    public float targetMaxScale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InteractiveEnvironmentObj>().onInteractionComplete += ScaleUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScaleUp()
    {
        transform.localScale = Vector3.one * Random.Range(targetMinScale, targetMaxScale);

        if(gameObject.GetComponent<GrowEvent>() && gameObject.GetComponent<GrowEvent>().growthProcessSteps.Length - 2 >= gameObject.GetComponent<GrowEvent>().currStep)
        {
            return;
        }
        else
        {
            Destroy(GetComponent<InteractiveEnvironmentObj>());
        }
    }
}
