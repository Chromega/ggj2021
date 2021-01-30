using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float bunnySpeed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float bunbunMove = Random.Range(0, 4);
        if (bunbunMove == 0)
        {
            transform.Translate(bunnySpeed, 0, 0);
        }
        else if (bunbunMove == 1)
        {
            transform.Translate(-bunnySpeed, 0, 0);
        }
        else if (bunbunMove == 2)
        {
            transform.Translate(0, 0, bunnySpeed);
        }
        else if (bunbunMove == 3)
        {
            transform.Translate(0, 0, -bunnySpeed);
        }
    }
}
