using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraModulator : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private void Awake()
    {
        vcam = transform.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        vcam.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        vcam.enabled = false;

    }
}
