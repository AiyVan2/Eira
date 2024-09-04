using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem hitEffect;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("BossPhaseTwo Took Damage: " + damage);
        StartCoroutine(blink());
        Camera.main.GetComponent<CameraShake>().TriggerShake();
        hitEffect.Play();

        if (health <= 0)
        {
            Debug.Log("BossPhaseTwo defeated");
        }
    }
    IEnumerator blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
