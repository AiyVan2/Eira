using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public GameObject[] constellationStars; // Drag the star GameObjects here in the inspector
    public GameObject[] incorrectConstellationStars; // Drag the incorrect star GameObjects here in the inspector
    public LineRenderer lineRenderer;

    private void Start()
    {
        // Set the constellation stars in each star script
        foreach (var star in constellationStars)
        {
            star.GetComponent<ConstellationStar>().SetConstellationStars(constellationStars);
        }
        // Set the incorrect stars in each star script (if necessary)
        foreach (var star in incorrectConstellationStars)
        {
            // You might want to directly reference these in the ConstellationStar script, 
            // or you can do it if your logic allows.
            star.GetComponent<ConstellationStar>().SetIncorrectStars(incorrectConstellationStars);
        }
    }
}
