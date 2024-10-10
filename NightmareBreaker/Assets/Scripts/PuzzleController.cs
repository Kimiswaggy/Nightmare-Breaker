using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public DoorController[] doors; // Array of doors that need to be activated
    public GameObject[] objectsToDisappear; // Objects to disappear when the puzzle is solved

    void Start()
    {
        // Optional: Automatically find all DoorController scripts if not set in the Inspector
        if (doors.Length == 0)
        {
            doors = FindObjectsOfType<DoorController>();

            if (doors == null || doors.Length == 0)
            {
                doors = FindObjectsOfType<DoorController>();
                Debug.Log("Doors found: " + doors.Length); // Add this line to debug the number of doors found

                if (doors.Length == 0)
                {
                    Debug.LogError("No DoorController objects found in the scene. Please assign the doors to the PuzzleController.");
                }
            }
        }
    }

    void Update()
    {
        if (AllDoorsActivated())
        {
            SolvePuzzle();
        }
    }

    bool AllDoorsActivated()
    {
        if (doors == null || doors.Length == 0)
        {
            Debug.LogError("No doors are assigned or found in the PuzzleController.");
            return false;
        }

        foreach (DoorController door in doors)
        {
            if (door == null)
            {
                Debug.LogWarning("One of the doors in the array is null.");
                continue; // Skip null doors
            }

            if (!door.isActivated)
            {
                Debug.Log("Door " + door.gameObject.name + " is not activated yet.");
                return false;
            }
        }

        Debug.Log("All doors are activated!");
        return true; // All doors are activated
    }


    void SolvePuzzle()
    {
        Debug.Log("Puzzle Solved!");

        foreach (GameObject obj in objectsToDisappear)
        {
            obj.SetActive(false); // Make objects disappear
        }

        // Disable this script to stop checking
        enabled = false;
    }
}
