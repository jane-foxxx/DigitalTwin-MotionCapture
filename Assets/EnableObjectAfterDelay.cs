using UnityEngine;
using System.Collections;

public class EnableObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToEnable; // Assign the target GameObject in the inspector
    public float delayInSeconds = 8f;  // Set the delay time in seconds in the inspector

    // Use this for initialization
    void Start()
    {
        objectToEnable.SetActive(false);
        // Start the coroutine to enable the object after the specified delay
        StartCoroutine(EnableAfterDelay());
    }

    IEnumerator EnableAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Enable the GameObject
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}