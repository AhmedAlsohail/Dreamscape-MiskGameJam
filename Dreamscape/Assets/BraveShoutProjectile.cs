using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveShoutProjectile : MonoBehaviour
{
    public float scaleRate = 0.1f; // Rate of scaling per second
    public int facingDir = 1; // Direction to scale (1 for right, -1 for left)

    private float initialScaleX;

    void Start()
    {
        // Store the initial scale on the X axis
        initialScaleX = transform.localScale.x;
    }

    void Update()
    {
        // Validate the facingDir value
        facingDir = Mathf.Clamp(facingDir, -1, 1);
        if (facingDir == 0) facingDir = 1; // Default to right if 0

        // Scale the object in the specified direction
        Vector3 newScale = transform.localScale;
        newScale.x += scaleRate * Time.deltaTime;
        transform.localScale = newScale;

        // Adjust the position to keep the growth in the specified direction
        Vector3 newPosition = transform.position;
        newPosition.x += (newScale.x - initialScaleX) * 0.5f * facingDir;
        transform.position = newPosition;
    }
}
