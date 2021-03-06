using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.25f;
    public float rakingSpeed = 1.0f;
    public GameObject playerModel;
    public GameObject wagon;
    public AudioClip pickupAudio;
    private Vector3 playerVelocity;
    private Animator animator;

    private Stack<GameObject> inventory;

    RakeController rakeController;
    CharacterController charController;
    EnvironmentInteractor myInteractor;
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        inventory = new Stack<GameObject>();
        wagon.SetActive(false);
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

        if ((horizontalInput != 0 || verticalInput != 0) && speedToUse > 0.0f)
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
            animator.SetBool("interacting", true);
        }
        else
        {
            if (myInteractor.interactionInProgress)
                myInteractor.CancelInteraction();
            isTryingToInteract = false;
            animator.SetBool("interacting", false);
        }

        if (Input.GetButtonDown("Taking"))
        {
            GameObject item = myInteractor.TakeNearbyObject();
            if (item != null)
            {
                AddToInventory(item);
                audioSource.PlayOneShot(pickupAudio, 0.25F);
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
        item.tag = "Untagged";
        item.transform.SetParent(wagon.transform);
        item.transform.localPosition = new Vector3(0, 0.25f, 0);
        wagon.SetActive(true);
    }

    public void PlaceFromInventory()
    {
        if (inventory.Count > 0)
        {
            GameObject item = inventory.Pop();
            float distanceToPlaceObject = 0.75f;


            

            GameObject campfire = GameObject.FindGameObjectWithTag("campfire");
            FireHealth campfireHealth = campfire.GetComponent<FireHealth>();
            float campfireDistance = Vector3.Distance(gameObject.transform.position, campfire.transform.position);
            if (campfireDistance < campfireHealth.maxEatDistance)
            {
                Destroy(item);
                campfireHealth.currentHealth++;
            }
            else
            {
                // Place the object in front of the character. This is SUPER hacky
                // for some reason the angle is off by 90 degrees lol.
                Vector3 forwardVector = playerModel.transform.forward;
                forwardVector = Quaternion.Euler(0, -90, 0) * forwardVector;
                Vector3 itemNewLoc = (forwardVector * distanceToPlaceObject) + transform.position;
                item.transform.position = itemNewLoc;
                item.transform.SetParent(null);
                item.tag = "interactiveEnvironment";
            }


            if (inventory.Count == 0) {
                wagon.SetActive(false);
            }           
        }
    }
}
