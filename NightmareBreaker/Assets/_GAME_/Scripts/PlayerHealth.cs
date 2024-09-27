using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public Animator animator;

    public float currentHealth, maxHealth = 8;
    private bool isDead = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }

    public void TakeDamage (int damgePoints)
    {
        if (currentHealth > 0 && !isDead)
        {
            currentHealth -= damgePoints;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                TriggerDeath();
            }
        }
    }

    public void Heal(int healPoints)
    {
        if (!isDead)
        {
            currentHealth += healPoints;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }

    private void TriggerDeath()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        Debug.Log("player is dead");
    }
}
