using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        //Get a handle to the player we hit
        var playerHit = collision.gameObject;
        var health = playerHit.GetComponent<playerHealth>();

        if(health != null)
        {
            health.TakeDamage(15);
        }
        else
        {
            Debug.Log("Something went terribly wrong");
        }

        Destroy(gameObject);
    }
}