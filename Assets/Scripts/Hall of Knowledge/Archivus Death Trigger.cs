using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ArchivusDeathTrigger : MonoBehaviour
{
    public GameObject Archivus;
    public GameObject Lever5;
    public GameObject bossroomBarrier;
    public GameObject thirdBook;

    public CameraSwap swap;
    private void Update()
    {
        if(Archivus == null)
        {
            Lever5.gameObject.SetActive(true);
            bossroomBarrier.gameObject.SetActive(false);
            thirdBook.gameObject.SetActive(false);
            swap.returntoPlayerCamera();
            
        }
    }
}
