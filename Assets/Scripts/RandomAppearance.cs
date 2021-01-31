using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppearance : MonoBehaviour
{
    public GameObject[] potentialAppearances;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject myNewAppearance = Instantiate(potentialAppearances[Random.Range(0, potentialAppearances.Length)], gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
