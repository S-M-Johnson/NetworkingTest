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

    [SyncVar(hook = "ChangeHealth")]
    public int currentHealth = maxHealth;

    public void TakeDamage(int damageTaken)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            //Die
            Debug.Log("You're dead, buddy");
        }
    }

    //This function is called automatically whenever currentHealth changes. The new value of currentHealth is passed in
    void ChangeHealth(int newHealth)
    {
        healthPercent = ((float)newHealth / 100f) * (float)maxHealth;
        Debug.Log(healthPercent);
        //Update healthbar
        healthBar.sizeDelta = new Vector2(healthPercent, healthBar.sizeDelta.y);
    }
}
