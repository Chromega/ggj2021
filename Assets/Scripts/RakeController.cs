using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeController : MonoBehaviour
{

    public static RakeController Instance { get; private set; }
    public GameObject rakeRoot;

    bool isRaking;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rakeRoot)
        {
            rakeRoot.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Raking"))
        {
            isRaking = true;
            rakeRoot.SetActive(true);
        }
        if (Input.GetButtonUp("Raking"))
        {
            isRaking = false;
            rakeRoot.SetActive(false);
        }
    }

    public bool IsRaking()
    {
        return isRaking;
    }
}
