using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    //public
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        //Update healthbar
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            //Die
            Debug.Log("You're dead, buddy");
        }
    }
}
