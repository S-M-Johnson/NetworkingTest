using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        //Get a handle to the player we hit
        var playerHit = collision.gameObject;
        var health = playerHit.GetComponent<PlayerHealth>();

        if(health != null)
        {
            health.TakeDamage(20);
        }
        else
        {
            //We hit something that doesn't have a health script
        }

        Destroy(gameObject);
    }
}