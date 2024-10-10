using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isActivated = false;

    // When the player presses 'E', activate the door
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerIsInRange()) // Check player proximity if needed
        {
            ActivateDoor();
        }
    }

    void ActivateDoor()
    {
        if (!isActivated)
        {
            isActivated = true;
            Debug.Log("Door " + gameObject.name + " is now activated.");
            // Play sound, animation, etc.
        }
    }

    bool PlayerIsInRange()
    {
        // Add your player proximity detection logic here, like checking distance
        return true; // For now, assuming the player is always in range
    }
}
