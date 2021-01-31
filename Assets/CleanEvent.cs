using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanEvent : MonoBehaviour
{
    public Material defaultMaterial;
    Material postMaterial;
    Material mMaterial;
    Renderer mRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InteractiveEnvironmentObj>().onInteractionComplete += ChangeMaterial;
        mRenderer = GetComponentInChildren<MeshRenderer>();
        postMaterial = mRenderer.material;
        mRenderer.material = defaultMaterial;
    }

    void ChangeMaterial()
    {
        mRenderer.material = postMaterial;
        Destroy(GetComponent<InteractiveEnvironmentObj>());
    }
}
