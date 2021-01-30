using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.25f;
    public float rakingSpeed = 1.0f;
    public GameObject playerModel;
    private Vector3 playerVelocity;

    RakeController rakeController;
    CharacterController charController;
    EnvironmentInteractor myInteractor;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rakeController = GetComponent<RakeController>();
        charController = GetComponent<CharacterController>();
        myInteractor = GetComponent<EnvironmentInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float speedToUse = (rakeController && rakeController.IsRaking()) ? rakingSpeed : speed;

        //transform.Translate(speedToUse * horizontalInput*Time.deltaTime, 0, speedToUse * verticalInput*Time.deltaTime);

        float ySpeed = charController.velocity.y;
        ySpeed += Physics.gravity.y * Time.deltaTime;

        float yOffset = charController.isGrounded?0:ySpeed * Time.deltaTime;
        charController.Move(new Vector3(speedToUse * horizontalInput * Time.deltaTime, yOffset, speedToUse * verticalInput * Time.deltaTime));

        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
                                                              Quaternion.LookRotation(
                                                              new Vector3(speed * verticalInput, 0, -speed * horizontalInput)),
                                                              Time.deltaTime * 30f);
        }

        if (Input.GetKey("space"))
        {
            myInteractor.InteractWithNearbySurroudings();
        }
        else if(myInteractor.interactionInProgress)
        {
            myInteractor.CancelInteraction();
        }
    }
}
