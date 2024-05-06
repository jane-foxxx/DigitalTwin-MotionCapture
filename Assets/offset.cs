using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class offset : MonoBehaviour
{
    public Transform target; // Reference to the main camera
    public float distance = 5f; // Fixed distance between the cube and the camera

    private void Update()
    {
        // Calculate the target position based on the camera's position and forward direction
        Vector3 targetPosition = target.position + target.forward * distance;

        // Set the cube's position to the target position
        transform.position = targetPosition;

        // Make the cube always face the camera
        transform.LookAt(target);
    }
}
/*
public class offset : MonoBehaviour
{
    public GameObject Target;
    public float smooth = 2f;
    Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(Target.transform.position + distance, transform.position, Time.deltaTime * smooth);
        //transform.LookAt(Target.transform.position);
    }

}
*/