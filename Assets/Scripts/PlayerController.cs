using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.25f;
    public float rakingSpeed = 1.0f;
    public GameObject playerModel;

    RakeController rakeController;

    // Start is called before the first frame update
    void Start()
    {
        rakeController = GetComponent<RakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float speedToUse = rakeController.IsRaking() ? rakingSpeed : speed;

        transform.Translate(speedToUse * horizontalInput*Time.deltaTime, 0, speedToUse * verticalInput*Time.deltaTime);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
                                                              Quaternion.LookRotation(
                                                              new Vector3(speed * verticalInput, 0, -speed * horizontalInput)),
                                                              Time.deltaTime * 30f);
        }
    }
}
