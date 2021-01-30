using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CameraZoom : MonoBehaviour
{
    // FOV Zoomimg
    /*private float minFov = 30f;
    private float maxFov = 60f;*/
    private float sensitivity = 10f;
    private float minZoom = -5f;
    private float maxZoom = -10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = transform.localPosition.z + Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        z = Mathf.Clamp(z, maxZoom, minZoom);
        transform.localPosition = new Vector3(0, 0, z);

        float change = Mathf.Abs(z)/12;
        DepthOfField dof;
        VolumeSettings.Instance.profile.TryGet<DepthOfField>(out dof);
        //9.18 and 13.9
        dof.nearFocusStart.value = 1 * change;
        dof.nearFocusEnd.value = 7f * change;
        dof.farFocusStart.value = 16f * change;
        dof.farFocusEnd.value = 20 * change;
        //FOV Zoom
        /*float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;*/
    }
}
