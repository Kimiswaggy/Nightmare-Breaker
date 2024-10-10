using System;
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

    private PlayerMovement playerMovement;
    public Vector2 respawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        respawnPoint = transform.position;
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
        playerMovement.enabled = false;
        Debug.Log("player is dead");
        StartCoroutine(Respawn());
    }

    public void SetRespawnPoint(Vector2 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint; // Update the respawn point
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f); // Delay before respawn

        if (respawnPoint != null)
        {
            // Move the player to the respawn point
            transform.position = respawnPoint;

            // Restore the player's health
            currentHealth = maxHealth;
            isDead = false;
            animator.SetBool("isDead", false);  // Reset the death animation
            animator.Play("Idle (NoWeapon)");
            playerMovement.enabled = true;

            Debug.Log("Player respawned at checkpoint");
        }
        else
        {
            Debug.LogWarning("No checkpoint set for respawn!");
        }
    }
}
