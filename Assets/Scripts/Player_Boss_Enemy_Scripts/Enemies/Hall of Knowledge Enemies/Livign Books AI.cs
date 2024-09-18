using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivignBooksAI : MonoBehaviour
{
    public GameObject player;
    public float followRange = 10f;
    public float speed = 5f;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= followRange)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            FollowPlayer();
        }
    }
}
