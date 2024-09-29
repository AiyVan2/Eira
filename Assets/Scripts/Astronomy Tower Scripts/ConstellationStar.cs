using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationStar : MonoBehaviour
{
   public LineRenderer lineRenderer; // Reference to the LineRenderer component
    private bool isConnected = false; // To track if the star has been activated
    private static int starsConnected = 0; // Track how many stars have been connected

    // This array will hold the stars to connect for Aries
    private static GameObject[] constellationStars;
    private static GameObject[] incorrectConstellationStars; // Set this from another script

    //Constellation Puzzle
    public GameObject constellationPuzzle;

    //One Way Platform Switch
    public GameObject platformSwitch;

    private void Start()
    {
        // Set up the LineRenderer
        lineRenderer.positionCount = 0; // Initially no points
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            // Check if this star is an incorrect one
            if (ArrayContains(incorrectConstellationStars, gameObject))
            {
                ResetStars(); // Reset all connected stars if this is an incorrect star
            }
            else
            {
                // If it's a correct star, we need to check the sequence
                int currentIndex = ArrayIndex(constellationStars, gameObject);
                
                // If the current star is the next one in the correct sequence
                if (currentIndex == starsConnected)
                {
                    GlowEffect();

                    if (!isConnected)
                    {
                        isConnected = true;
                        starsConnected++;

                        // Add this star's position to the LineRenderer
                        DrawLine();

                        // Check if the entire constellation is connected
                        if (starsConnected == constellationStars.Length)
                        {
                            CompleteConstellation();
                        }
                    }
                }
                else
                {
                    Debug.Log("This is not the correct star in sequence!");
                }
            }
        }
    }

    private void GlowEffect()
    {
        // Change color to indicate activation (example: to yellow)
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void DrawLine()
    {
        lineRenderer.positionCount = starsConnected;

        // Set the position of the line to this star's position
        lineRenderer.SetPosition(starsConnected - 1, transform.position);
    }

    private void CompleteConstellation()
    {
        // Logic for completing the constellation (unlock door, etc.)
        platformSwitch.SetActive(true);
        constellationPuzzle.SetActive(false);
        Debug.Log("Constellation complete! Unlocking the door...");
    }

    private void ResetStars()
    {
        starsConnected = 0; // Reset the counter
        // Reset each star in the constellation
        foreach (var star in constellationStars)
        {
            var constellationStar = star.GetComponent<ConstellationStar>();
            if (constellationStar != null)
            {
                constellationStar.isConnected = false;
                constellationStar.GetComponent<Renderer>().material.color = Color.white; // Reset to original color
                constellationStar.lineRenderer.positionCount = 0; // Clear the line
            }
        }
        Debug.Log("Incorrect star hit! Resetting constellation.");
    }

    // Method to set the array of stars (should be called from another manager script)
    public void SetConstellationStars(GameObject[] stars)
    {
        constellationStars = stars;
    }
    public void SetIncorrectStars(GameObject[] stars)
    {
        incorrectConstellationStars = stars;
    }

    private bool ArrayContains(GameObject[] array, GameObject obj)
    {
        foreach (var item in array)
        {
            if (item == obj) return true;
        }
        return false;
    }

    private int ArrayIndex(GameObject[] array, GameObject obj)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == obj)
                return i;
        }
        return -1; // Return -1 if not found
    }
}