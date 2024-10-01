using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwap : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera playerCamera; // Assign your default Virtual Camera here
    public CinemachineVirtualCamera bossFightCamera; // Assign your Colossal Boss Fight Camera here

    public void swaptoboosRoom()
    {
        bossFightCamera.Priority = 11;
        playerCamera.Priority = 10; // Lower priority for the normal camera
    }
    public void returntoPlayerCamera()
    {
        bossFightCamera.Priority = 9;
        playerCamera.Priority= 10;
    }
}
