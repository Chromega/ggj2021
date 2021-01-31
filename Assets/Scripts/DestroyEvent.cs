using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InteractiveEnvironmentObj>().onInteractionComplete += TimeToDie;
    }

    void TimeToDie()
    {
        Destroy(gameObject);
    }
}
