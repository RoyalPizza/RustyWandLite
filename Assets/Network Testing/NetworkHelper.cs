using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkHelper : NetworkBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    private void Start()
    {
        if (!IsLocalPlayer)
        {
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;
        }
        else
        {
            cameraTransform.GetComponent<AudioListener>().enabled = true;
            cameraTransform.GetComponent<AudioListener>().enabled = true;
        }
    }

    public bool GetLocalPlayer()
    {
        return IsLocalPlayer;
    }
}
