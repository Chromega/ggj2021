using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.25f;
    public float rakingSpeed = 1.0f;
    public GameObject playerModel;
    private Vector3 playerVelocity;
    private Animator animator;

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
        animator = GetComponent<Animator>();
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
            animator.SetBool("moving", true);

            float xzSpeed = (new Vector2(speedToUse * horizontalInput, speedToUse * verticalInput)).magnitude;
            animator.SetFloat("walk_speed", xzSpeed*.75f);
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
                                                              Quaternion.LookRotation(
                                                              new Vector3(speed * verticalInput, 0, -speed * horizontalInput)),
                                                              Time.deltaTime * 30f);
        }
<<<<<<< HEAD
        else
        {
            animator.SetBool("moving", false);
=======

        if (Input.GetKey("space"))
        {
            myInteractor.InteractWithNearbySurroudings();
        }
        else if(myInteractor.interactionInProgress)
        {
            myInteractor.CancelInteraction();
>>>>>>> 241f87f3cdbec2313498a17c6305e8a6ce8c655d
        }
    }
}
