using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMoveTest : NetworkBehaviour {

    //public
    public GameObject bullet;
    public Transform bulletSpawnLocation;

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Space))
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
