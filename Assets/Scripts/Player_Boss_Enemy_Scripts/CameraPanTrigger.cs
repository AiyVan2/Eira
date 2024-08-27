using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraPanTrigger : MonoBehaviour
{
   
    public CinemachineVirtualCamera virtualCamera;

    public GameObject targetObject;

    public float panDuration = 2.0f;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        virtualCamera.Follow = playerTransform; // Set the initial follow target
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the player collided with the trigger
        if (other.gameObject.tag == "Player")
        {
            // Pan the camera to the target object
            virtualCamera.Follow = targetObject.transform;

            // Wait for the pan duration
            Invoke("ReturnToPlayer", panDuration);
        }
    }

    void ReturnToPlayer()
    {
        // Return the camera to the player
        virtualCamera.Follow = playerTransform;
    }
}
