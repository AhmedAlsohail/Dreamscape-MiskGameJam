using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Grower : MonoBehaviour
{
    public Transform target; // Target to touch
    public float growthRate = 0.01f; // Rate of growth
    public float touchRadius = 0.2f; // Radius to consider a touch with the target
    public float waitTime = 3f; // Time to wait before starting to grow

    private CircleCollider2D circleCollider;
    private float originalRadius;
    private float timer;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform; // Find target by tag if needed
        circleCollider = GetComponent<CircleCollider2D>();
        originalRadius = circleCollider.radius;
        timer = waitTime; // Set timer to the wait time
    }

    void Update()
    {
        if (timer > 0)
        {
            // Reduce the timer
            timer -= Time.deltaTime;
        }
        else
        {
            // Grow the circle
            circleCollider.radius += growthRate * Time.deltaTime;

            // Scale the object's transform to match the collider's size
            transform.localScale = Vector3.one * (circleCollider.radius / originalRadius);

            // Check if the circle is touching the target
            if (Vector2.Distance(transform.position, target.position) <= circleCollider.radius + touchRadius)
            {
                HitTarget();
                // You can add more actions here, like stopping the growth or destroying the target, etc.
            }
        }
    }

    public void HitTarget()
    {
        Destroy(gameObject);
    }
}
