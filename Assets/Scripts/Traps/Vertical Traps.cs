using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTraps : MonoBehaviour
{
    public Transform PosA;
    public Transform PosB;
    private float moveSpeed = 4f;


    Vector2 targetPos;



    private void Start()
    {
        targetPos = PosB.position;
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, PosA.position)<0.1f) targetPos = PosB.position;
        if(Vector2.Distance(transform.position, PosB.position)<0.1f) targetPos = PosA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

}
