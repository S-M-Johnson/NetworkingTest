using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LookAt(float rotationY)
    {
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
