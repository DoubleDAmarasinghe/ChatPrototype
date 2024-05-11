using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector2 areaSize; // Size of the area where the camera should follow the player

    public float smoothSpeed = 0.125f; // Speed of camera movement

    private void LateUpdate()
    {
        // Get the position of the camera
        Vector3 desiredPosition = transform.position;

        // Check if the player has exceeded the area bounds
        if (player.position.x < transform.position.x - areaSize.x / 2 ||
            player.position.x > transform.position.x + areaSize.x / 2 ||
            player.position.y < transform.position.y - areaSize.y / 2 ||
            player.position.y > transform.position.y + areaSize.y / 2)
        {
            // Smoothly move the camera towards the player
            desiredPosition = new Vector3(player.position.x, transform.position.y, player.position.z/2);
        }

        // Interpolate the position of the camera for smooth movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the position of the camera
        transform.position = smoothedPosition;
    }
}
