using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivusDeathTrigger : MonoBehaviour
{
    public GameObject Archivus;
    public GameObject Lever5;
    public GameObject bossroomBarrier;

    private void Update()
    {
        if(Archivus == null)
        {
            Lever5.gameObject.SetActive(true);
            bossroomBarrier.gameObject.SetActive(false);
            
        }
    }
}
