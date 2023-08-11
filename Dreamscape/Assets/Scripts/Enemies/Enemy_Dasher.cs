using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dasher : MonoBehaviour
{
    public Transform target; // Target to dash towards
    public float dashSpeed = 5f; // Speed of dashing
    public float waitTime = 1f; // Time to wait before dashing
    public float continueDistance = 50f; // Distance to continue moving after reaching target
    public float rotationSpeed = 180f; // Speed of rotation

    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isDashing;
    private float timer;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform; // Find target by tag if needed
        startPosition = transform.position;
        Vector2 directionToTarget = (Vector2)target.position - startPosition;
        directionToTarget.Normalize(); // Get the unit direction towards the target
        endPosition = (Vector2)target.position + directionToTarget * continueDistance; // Position 50 units forward from the target

        timer = waitTime;
        isDashing = false;

        // Rotate towards the target at the start
        RotateTowardsTarget();
    }

    void Update()
    {
        if (!isDashing && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isDashing = true; // Start dashing
            }

            // Continue to rotate towards the target while waiting
            RotateTowardsTarget();
        }

        if (isDashing)
        {
            // Dash towards the target and beyond
            float step = dashSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(transform.position, endPosition, step);
            transform.position = newPosition;

            // Check if reached the end position
            if (newPosition == endPosition)
            {
                Debug.Log("Reached Destination!");
                isDashing = false;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 directionToTarget = (Vector2)target.position - startPosition;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
