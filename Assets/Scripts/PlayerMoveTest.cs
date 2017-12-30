using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMoveTest : NetworkBehaviour {

    //public
    public GameObject bullet;
    public Transform bulletSpawnLocation;
    public Camera playerCam;
    public AudioListener playerListener;
    public Canvas healthBarCanvas;

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
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 4.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 4.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        //Spawn a bullet
        var newBullet = (GameObject)Instantiate(bullet, bulletSpawnLocation.position, bulletSpawnLocation.rotation);

        //Give the new bullet some velocity
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 6;

        //Also spawn this bullet on the clients
        NetworkServer.Spawn(newBullet);

        //Bullets will self destruct in 3 seconds
        Destroy(newBullet, 3.0f);
    }
}
