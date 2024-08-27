using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{


    public GameObject player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f; // Adjust this value to control the smoothness of the camera movement

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player"); // Find the player GameObject by name
        }

        if (player != null)
        {
            offset = transform.position - player.transform.position; // Calculate and store the offset
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure the player GameObject is named 'Player'.");
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        }
    }
}