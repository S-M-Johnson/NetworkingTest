using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2};
    public RotationAxes axes = RotationAxes.MouseXandY;
    public float sensitivity = 15f;
    float rotationX = 0f;
    float rotationY = 0f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else
        {
            rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

            rotationY += Input.GetAxis("Mouse Y") * sensitivity;

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
	}
}
