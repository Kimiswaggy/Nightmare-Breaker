using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;

    public float currentHealth, maxHealth = 8;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth > maxHealth)currentHealth = maxHealth;
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }

    public void Damage (int damgePoints)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damgePoints;
        }
    }

    public void Heal (int healPoints)
    {
        currentHealth += healPoints;
    }
}
