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

    private Stack<GameObject> inventory;

    RakeController rakeController;
    CharacterController charController;
    EnvironmentInteractor myInteractor;

    bool isTryingToInteract = false;

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
        inventory = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float speedToUse = (rakeController && rakeController.IsRaking()) ? rakingSpeed : speed;
        if (isTryingToInteract)
            speedToUse = 0.0f;

        //transform.Translate(speedToUse * horizontalInput*Time.deltaTime, 0, speedToUse * verticalInput*Time.deltaTime);

        float ySpeed = charController.velocity.y;
        ySpeed += Physics.gravity.y * Time.deltaTime;

        float yOffset = charController.isGrounded?0:ySpeed * Time.deltaTime;
        charController.Move(new Vector3(speedToUse * horizontalInput * Time.deltaTime, yOffset, speedToUse * verticalInput * Time.deltaTime));

        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("moving", true);

            float xzSpeed = (new Vector2(speedToUse * horizontalInput, speedToUse * verticalInput)).magnitude;
            animator.SetFloat("walk_speed", xzSpeed * .75f);
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,
                                                              Quaternion.LookRotation(
                                                              new Vector3(speed * verticalInput, 0, -speed * horizontalInput)),
                                                              Time.deltaTime * 30f);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if (Input.GetButton("Interact"))
        {
            myInteractor.InteractWithNearbySurroudings();
            isTryingToInteract = true;
        }
        else
        {
            if (myInteractor.interactionInProgress)
                myInteractor.CancelInteraction();
            isTryingToInteract = false;
        }

        if (Input.GetButtonDown("Taking"))
        {
            GameObject item = myInteractor.TakeNearbyObject();
            if (item != null)
            {
                AddToInventory(item);
            } else
            {
                // no items to take
                PlaceFromInventory();
            }
        }
    }

    public void AddToInventory(GameObject item)
    {
        inventory.Push(item);
        item.SetActive(false);
    }

    public void PlaceFromInventory()
    {
        if (inventory.Count > 0)
        {
            GameObject item = inventory.Pop();

            // Place the object in front of the character. This is SUPER hacky
            // for some reason the angle is off by 90 degrees lol.
            Vector3 forwardVector = playerModel.transform.forward;
            forwardVector = Quaternion.Euler(0, -90, 0) * forwardVector;
            Vector3 itemNewLoc = (forwardVector * 1.0f) + transform.position;
            item.transform.position = itemNewLoc;

            // Re-enable the object
            item.SetActive(true);
        }
    }
}
