using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPosition;
    private bool checkpointSet = false;

    void Start()
    {
        // Initialize the starting position as the first checkpoint
        respawnPosition = transform.position;
        checkpointSet = true;  // Set the checkpoint flag to true
    }

    public void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        respawnPosition = newCheckpoint;
        checkpointSet = true;  // Mark checkpoint as set
        Debug.Log("Checkpoint updated!");
    }

    public void Respawn()
    {
        if (checkpointSet)
        {
            transform.position = respawnPosition;

            // Restore the player's health
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(8);  // Restore full health
            }

            Debug.Log("Player respawned at checkpoint");
        }
        else
        {
            Debug.LogWarning("No checkpoint set for respawn!");
        }
    }
}