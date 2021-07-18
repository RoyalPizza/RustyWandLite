using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public float speed = 5.0F;
    public float rotateSpeed = 1.0F;
    public float pitch = 0f;

    CharacterController characterController;
    Transform cameraTransform;

    private void Start()
    {
        if (!IsLocalPlayer)
        {
            cameraTransform = transform.GetChild(1).GetComponent<Transform>();
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;
        }
        else
        {
            characterController = GetComponent<CharacterController>();
            cameraTransform = transform.GetChild(1).GetComponent<Transform>();
            cameraTransform.GetComponent<AudioListener>().enabled = true;
            cameraTransform.GetComponent<AudioListener>().enabled = true;
        }
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            Move();
            Look();
        }
    }

    private void Move()
    {

        // Rotate around y - axis
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        characterController.SimpleMove(forward * curSpeed);
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0, mouseX, 0);
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}