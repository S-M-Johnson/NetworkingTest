using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playerHealth : NetworkBehaviour {

    //public
    public const int maxHealth = 100;
    public RectTransform healthBar;
    public float healthPercent = 0;
    public float healthBarX = 0;

    [SyncVar(hook = "ChangeHealth")]
    public int currentHealth = maxHealth;

    //private
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        //Get length of health bar for percentage calculation
        healthBarX = healthBar.sizeDelta.x;

        //Get all the spawn points
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
    }

    public void TakeDamage(int damageTaken)
    {
        if(!isServer)
        {
            return;
        }
        Debug.Log("Starting health: " + currentHealth);
        currentHealth -= damageTaken;
        Debug.Log("Ending health: " + currentHealth);
        if (currentHealth <= 0)
        {
            //Respawn (immediately, for now)
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    //This function is called automatically whenever currentHealth changes. The new value of currentHealth is passed in
    void ChangeHealth(int newHealth)
    {
        healthPercent = (newHealth / 100f) * healthBarX;
        Debug.Log(newHealth);

        //Update healthbar
        healthBar.sizeDelta = new Vector2(healthPercent, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        Vector3 spawnLocation = Vector3.zero;
        if(spawnPoints != null && spawnPoints.Length > 0)
        {
            spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        transform.position = spawnLocation;
    }
}
