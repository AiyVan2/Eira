using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPlatform : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public SpriteRenderer platform1;
    public SpriteRenderer platform2;
    public SpriteRenderer platform3;
    public SpriteRenderer platform4;
    public SpriteRenderer platform5;

    public bool visible = false;

    public CameraSwap swap;
    private void Start()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if (!visible)
            {
                StartCoroutine(showPlatforms());
            }
            else
            {
                Debug.Log("hinay hinay");
            }


        }
    }
    IEnumerator showPlatforms()
    {
        swap.swaptoboosRoom();
        spriteRenderer.color = Color.cyan;
        visible = true;
        platform1.enabled = true;
        platform2.enabled = true;
        platform3.enabled = true;
        platform4.enabled = true;
        platform5.enabled = true;
        yield return new WaitForSeconds(5f);
        platform1.enabled = false;
        platform2.enabled = false;
        platform3.enabled = false;
        platform4.enabled = false;
        platform5.enabled = false;
      
        spriteRenderer.color = Color.white;
        swap.returntoPlayerCamera();
        visible = false;
    }
}
