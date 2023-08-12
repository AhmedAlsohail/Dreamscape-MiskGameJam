using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : MonoBehaviour
{
    public Transform target; // Target to follow
    public float speed = 5f; // Speed of movement
    public float randomness = 0.5f; // Amount of randomness in movement

    public AudioClip wingstSound; // Assign the jump sound effect in the Inspector
    private SoundEffectManager soundEffectManager;
    void Start()
    {
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
        soundEffectManager.PlaySoundEffect(wingstSound);
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            // Get the direction from the object to the target
            Vector3 directionToTarget = target.position - transform.position;

            // Check if the target is on the left or right
            if (directionToTarget.x < 0)
            {
                // Flip the object
                transform.localScale = new Vector3(-1, 1, 1); // Flipped along the X-axis
            }
            else
            {
                // Unflip the object
                transform.localScale = new Vector3(1, 1, 1); // Normal scale
            }

            // Calculate the direction towards the target
            directionToTarget = (target.position - transform.position).normalized;

            // Add some randomness to the direction
            Vector3 randomDirection = new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));
            Vector3 finalDirection = (directionToTarget + randomDirection).normalized;

            // Calculate the new position
            Vector3 newPosition = transform.position + finalDirection * speed * Time.deltaTime;

            // Move to the new position
            transform.position = newPosition;
        }
    }


}
