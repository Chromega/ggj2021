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
    private float sensitivity = 5f;
    private float minZoom = -5f;
    private float maxZoom = -10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!VolumeSettings.Instance)
            return;

        float z = transform.localPosition.z + Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        z = Mathf.Clamp(z, maxZoom, minZoom);
        transform.localPosition = new Vector3(0, 0, z);

        float change = Mathf.Abs(z)/10;
        DepthOfField dof;
        VolumeSettings.Instance.profile.TryGet<DepthOfField>(out dof);
        //9.18 and 13.9
        dof.nearFocusStart.value = 1f * change;
        dof.nearFocusEnd.value = 9f * change;
        dof.farFocusStart.value = 16f * change;
        dof.farFocusEnd.value = 20f * change;
        /*dof.nearFocusEnd.value = Mathf.Clamp(dof.nearFocusEnd.value, 4, 8);
        dof.farFocusStart.value = Mathf.Clamp(dof.farFocusStart.value, 10, 18);
        dof.farFocusEnd.value = Mathf.Clamp(dof.farFocusEnd.value, 16, 20);*/
        //FOV Zoom
        /*float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;*/
    }
}
