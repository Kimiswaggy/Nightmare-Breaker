using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    private void Start()
    {
        // Find the PlayerHealth and PlayerMovement components attached to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the death zone
        if (collision.CompareTag("Player"))
        {
            // Only kill the player if they are NOT invulnerable
            if (!playerMovement.IsInvulnerable())
            {
                Debug.Log("Player entered death zone");

                // Trigger the player's death
                playerHealth.TakeDamage((int)playerHealth.maxHealth); // Kill the player instantly
            }
            else
            {
                Debug.Log("Player is invulnerable during dash, no death triggered.");
            }
        }
    }
}
