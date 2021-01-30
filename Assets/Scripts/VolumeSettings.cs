using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeSettings : MonoBehaviour
{
    public static Volume Instance { get; private set; }

    private void Awake()
    {
        Instance = GetComponent<Volume>();
    }
}
