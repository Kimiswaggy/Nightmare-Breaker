using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private AudioSource audioSource;

    void Start()
    {
        // Find the PlayerHealth script attached to the player
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();

        // Debug log to confirm AudioSource is assigned correctly
        if (audioSource != null)
        {
            Debug.Log("AudioSource found on Checkpoint object");
        }
        else
        {
            Debug.LogWarning("No AudioSource found on Checkpoint object");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches a tagged checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Update the player's respawn point in PlayerHealth
            playerHealth.SetRespawnPoint(other.transform.position);
            Debug.Log("Checkpoint updated: " + other.transform.position);

            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Playing checkpoint sound");
            }
        }
    }
}
