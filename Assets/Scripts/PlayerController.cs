using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.25f;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(speed * horizontalInput, 0, speed * verticalInput);

        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, 
                                                          Quaternion.LookRotation(
                                                          new Vector3(speed * verticalInput, 0, -speed * horizontalInput)), 
                                                          Time.deltaTime * 30f);
    }
}
