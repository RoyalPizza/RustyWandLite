using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public bool SimpleControls = false;

    public float speed = 5.0F;
    public float rotateSpeed = 1.0F;
    public float pitch = 0f;

    CharacterController characterController;
    Transform cameraTransform;
    Rigidbody _rigidbody;


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
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            if (SimpleControls)
            {
                SimpleMove();
                SimpleLook();
            }
            else
            {
                //Store user input as a movement vector
                Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                var curSpeed = input * speed;

                //Apply the movement vector to the current position, which is
                //multiplied by deltaTime and speed for a smooth MovePosition
                _rigidbody.MovePosition(transform.position + input * speed * Time.deltaTime);

                SimpleLook();
            }
        }
    }

    private void Move()
    {

    }

    private void SimpleMove()
    {
        // Move forward / backward
        if (Input.GetAxis("Vertical") > 0f)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            characterController.SimpleMove(forward * curSpeed);
        }
    }

    private void SimpleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0, mouseX, 0);
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}