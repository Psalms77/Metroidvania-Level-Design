using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOdor : Observer
{
    public float minScale = 0.5f; // Minimum scale value
    public float maxScale = 2f;   // Maximum scale value
    public float scaleSpeed = 1f; // Scaling speed

    private bool scalingUp = true; // Flag to determine whether to scale up or down

    void Update()
    {
        // Scale the object based on the scaling speed
        float scaleFactor = scalingUp ? 1 + Time.deltaTime * scaleSpeed : 1 - Time.deltaTime * scaleSpeed;
        transform.localScale *= scaleFactor;

        // Check if the object's scale exceeds the specified bounds
        if (transform.localScale.x > maxScale)
        {
            scalingUp = false;
        }
        else if (transform.localScale.x < minScale)
        {
            scalingUp = true;
        }
    }
}
