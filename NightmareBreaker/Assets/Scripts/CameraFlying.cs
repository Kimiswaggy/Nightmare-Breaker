using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlying : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float scrollSpeed = 20f;
    [SerializeField] private float zoomSpeed = 5f;

    private void Update()
    {
        // Handle camera movement using arrow keys or WASD
        HandleMovement();

        // Handle zoom in and out with mouse scroll
        HandleZoom();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = transform.position + new Vector3(0, 0, scroll * zoomSpeed);
        transform.position = zoom;
    }
}
