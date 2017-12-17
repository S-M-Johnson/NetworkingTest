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

    void Start()
    {
        healthBarX = healthBar.sizeDelta.x;
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
        if(isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
