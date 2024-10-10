using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        // Find the PlayerHealth script attached to the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches a tagged checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Update the player's respawn point in PlayerHealth
            playerHealth.SetRespawnPoint(other.transform.position);
            Debug.Log("Checkpoint updated: " + other.transform.position);
        }
    }
}
