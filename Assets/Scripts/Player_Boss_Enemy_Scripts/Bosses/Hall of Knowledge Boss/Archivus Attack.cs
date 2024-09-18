using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float lifespan = 1f;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

}
