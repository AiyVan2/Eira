using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cinemachine;
using UnityEngine;

public class LeverScript: MonoBehaviour
{
    public enum Levers { Lever1, Lever2, Lever3, Lever4, Lever5 }
    public Levers currentLever;

    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject Door5;

    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;
    public GameObject lever5;

    // Cinemachine camera reference
    public CinemachineVirtualCamera cinemachineCam;

    // Targets for panning when doors open
    public Transform door1PanTarget;
    public Transform door2PanTarget;
    public Transform door3PanTarget;
    public Transform door4PanTarget;
    public Transform door5PanTarget;

    private Transform originalCamTarget;
    public float panDuration = 2.0f; // Duration of the pan

    private void Start()
    {
        // Save the initial camera follow target (the player, typically)
        originalCamTarget = cinemachineCam.Follow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            switch (currentLever)
            {
                case Levers.Lever1:
                    StartCoroutine(ActivateLeverAndPan(1, door1PanTarget, Door1, lever1));
                    break;
                case Levers.Lever2:
                    StartCoroutine(ActivateLeverAndPan(2, door2PanTarget, Door2, lever2));
                    break;
                case Levers.Lever3:
                    StartCoroutine(ActivateLeverAndPan(3, door3PanTarget, Door3, lever3));
                    break;
                case Levers.Lever4:
                    StartCoroutine(ActivateLeverAndPan(4, door4PanTarget, Door4, lever4));
                    break;
                case Levers.Lever5:
                    StartCoroutine(ActivateLeverAndPan(5, door5PanTarget, Door5, lever5));
                    break;
            }
        }
    }

    // Coroutine to pan the camera, deactivate the door and lever, and return the camera to its original position
    private IEnumerator ActivateLeverAndPan(int leverIndex, Transform panTarget, GameObject door, GameObject lever)
    {
        // Pan to the door
        cinemachineCam.Follow = panTarget;
        yield return new WaitForSeconds(panDuration);

        // Deactivate the door and lever
        door.SetActive(false);
        lever.SetActive(false);

        // Return the camera to the original position (typically the player)
        cinemachineCam.Follow = originalCamTarget;
    }
}

