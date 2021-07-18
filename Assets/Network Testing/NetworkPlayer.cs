using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public float speed = 5.0F;
    public float rotateSpeed = 1.0F;

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            // Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            characterController.SimpleMove(forward * curSpeed);
        }
    }
}