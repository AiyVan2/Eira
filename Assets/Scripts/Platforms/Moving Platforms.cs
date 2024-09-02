using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints the platform will move between
    public float moveSpeed = 2f; // Speed of the platform's movement
    public int startingPoint = 0; // Starting point index for the platform

    private int waypointIndex; // Current waypoint index

    void Start()
    {
        waypointIndex = startingPoint;
        transform.position = waypoints[waypointIndex].position; // Set initial position of the platform
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (waypoints.Length == 0)
            return;

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            // Update the waypoint index
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
