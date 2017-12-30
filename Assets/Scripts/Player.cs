using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    //public
    public GameObject bullet;
    public Transform bulletSpawnLocation;
    public Camera playerCam;
    public AudioListener playerListener;
    public Canvas healthBarCanvas;
    public float speed = 6f;
    public float bulletSpeed = 10f;

    //private
    private Vector3 movement;
    private Rigidbody playerRB;

    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Get and activate the player's camera
        playerCam = GetComponentInChildren<Camera>();
        playerCam.enabled = true;

        //Get and activate the player's audiolistener
        playerListener = GetComponentInChildren<AudioListener>();
        playerListener.enabled = true;

        //Get and activate the player's healthbar
        healthBarCanvas = GetComponentInChildren<Canvas>();
        healthBarCanvas.enabled = true;

        //Get the player's rigidbody
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot();
        }
    }

    void FixedUpdate () {
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        Move(hMove, vMove);
    }

    [Command]
    void CmdShoot()
    {
        //Spawn a bullet
        var newBullet = (GameObject)Instantiate(bullet, bulletSpawnLocation.position, bulletSpawnLocation.rotation);

        //Give the new bullet some velocity
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;

        //Also spawn this bullet on the clients
        NetworkServer.Spawn(newBullet);

        //Bullets will self destruct in 3 seconds
        Destroy(newBullet, 3.0f);
    }

    void Move(float hMove, float vMove)
    {
        movement.Set(0f, 0f, 0f);
        movement += vMove * transform.forward;
        movement += hMove * transform.right;

        movement = movement.normalized * speed * Time.deltaTime;        //normalized prevents moving faster diagonally

        playerRB.MovePosition(transform.position + movement);
    }
}
