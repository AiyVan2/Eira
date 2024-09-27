using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Transform location;   
    public float speed = 6f;     

    private Vector2 targetPos;  
    private bool shouldMove = false; 

    private void Start()
    {
        targetPos = location.position;
    }

    public void StartElevator()
    {
        shouldMove = true;
        StartCoroutine(MovePlatform());
    }

   
    private IEnumerator MovePlatform()
    {
        while (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
          
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                shouldMove = false; 
            }

            yield return null;
        }
    }
}