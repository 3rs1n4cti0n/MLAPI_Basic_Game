using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class playerMovement : NetworkBehaviour
{
    CharacterController controller;
    public Transform cam;
    float pitch;

    public void Start()
    {
        if (!IsLocalPlayer)
        {
            cam.GetComponent<AudioListener>().enabled = false;
            cam.GetComponent<Camera>().enabled = false;
        }
        else
        {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (IsLocalPlayer && controller.enabled == true)
        {
            Moveplayer();
            Look();
        }
    }

    void Moveplayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f);
        move = transform.TransformDirection(move);
        controller.Move(move * 5f * Time.deltaTime);
    }

    void Look()
    {
        float mousex = Input.GetAxis("Mouse X") * 3f;
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        transform.Rotate(0, mousex, 0);
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        cam.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
