using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransformMatcher : MonoBehaviour
{
    public Transform targetTransform; // Assign this in the Inspector with the transform of object A

    void Update()
    {
        // Match position and rotation with the target transform every frame
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}