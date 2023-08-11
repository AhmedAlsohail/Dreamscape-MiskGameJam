using UnityEngine;

public class Enemy_Jumper : MonoBehaviour
{
    public Transform target; // Target to follow
    public float speed = 5f; // Speed of movement
    public float jumpForce = 5f; // Force of the jump
    public float jumpDistance = 2f; // Distance at which the object will jump
    public float noiseStrength = 0.5f; // Strength of the noise
    public LayerMask whatIsGround; // Layer to define what is ground
    public float groundCheckRadius = 0.2f; // Radius to check if on ground

    private Rigidbody2D rb;
    public bool isGrounded;
    private float noiseOffset;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        noiseOffset = Random.Range(0f, 100f); // Randomize noise offset
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the horizontal distance to the target
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (target.position.y > transform.position.y && distanceToTarget < jumpDistance && isGrounded)
            {
                // Jump towards the target
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            // Move horizontally towards the target
            float step = speed * Time.deltaTime;
            float newPosX = Mathf.MoveTowards(transform.position.x, target.position.x, step);

            // Add noise to the horizontal movement
            float noise = Mathf.PerlinNoise(noiseOffset, 0f) * 2f - 1f; // Returns value between -1 and 1
            noiseOffset += 0.01f; // Increment offset to progress through noise
            newPosX += noise * noiseStrength;

            rb.position = new Vector2(newPosX, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}