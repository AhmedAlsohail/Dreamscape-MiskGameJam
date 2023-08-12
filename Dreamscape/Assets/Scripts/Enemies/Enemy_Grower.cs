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

    Transform child;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform; // Find target by tag if needed
        circleCollider = GetComponent<CircleCollider2D>();
        originalRadius = circleCollider.radius;
        timer = waitTime; // Set timer to the wait time
        child = transform.Find("Animation"); // Replace "ChildName" with the actual name of the child
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
            // Grow the collider
            circleCollider.radius += growthRate * Time.deltaTime;

            // Update the object's scale based on the collider's size
            float scaleFactor = circleCollider.radius / originalRadius;
            child.localScale = new Vector3(0.2f * scaleFactor, 0.2f * scaleFactor, 1f);
        }
    }
}
