using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //public
        public float speed = 6f;

    //private
        private Vector3 movement;
        private Rigidbody playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");
	}

    void Move(float hMove, float vMove)
    {
        movement.Set(hMove, 0f, vMove);

        movement = movement.normalized * speed * Time.deltaTime;        //normalized prevents moving faster diagonally

        playerRB.MovePosition(transform.position + movement);
    }
}
