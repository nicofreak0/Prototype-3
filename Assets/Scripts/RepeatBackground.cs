using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;

    void Start()
    {
        // Save the starting position to reset the background position
        startPosition = transform.position;

        // Get the width of the background using the BoxCollider's size to know when to repeat
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // Check if the background has moved left far enough to reset
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            // Reset the position to the starting position
            transform.position = startPosition;
        }
    }
}
