using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MouseLook : NetworkBehaviour
{

    //public
    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXandY;
    public LookHere playerLookDirection;
    public float sensitivity = 15f;

    //private
    private float rotationY = 0f;
    private float minY = -80f;
    private float maxY = 80f;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            //Get the player's look script
            playerLookDirection = GetComponentInChildren<LookHere>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //x part
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

            //y part
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

        }

        //Tell the Camera/Gun where to look
        playerLookDirection.LookAt(rotationY);
        
    }
}