using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sniper : MonoBehaviour
{
    public Transform target;
    public CameraEffects camera;
    public float speed = 5.0f;
    public float touchRadius = 0.2f; // Radius to consider a touch with the target

    public float lifeTime;
    public float currentLifeTime;

    public float moveTime;
    public float currentMoveTime;

    public float durationToTarget = 0.3f;

    private bool isMoving = false; // Flag to control movement

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraEffects>();
        currentLifeTime = lifeTime;
        currentMoveTime = moveTime;
    }

    void Update()
    {

        if(currentMoveTime <= 0)
        {
            currentMoveTime = moveTime;
            StartCoroutine(MoveToTarget());
        }
        else
        {
            currentMoveTime -= Time.deltaTime;
        }
        
        if(currentLifeTime <= 0)
        {
            shoot();
        }
        else
        {
            currentLifeTime -= Time.deltaTime;
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        Vector3 initialPosition = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < durationToTarget)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation value (0 to 1)
            float t = Mathf.Clamp01(elapsedTime / durationToTarget);

            // Smoothly move the object from initialPosition to target position
            transform.position = Vector3.Lerp(initialPosition, target.position, t);

            yield return null; // Wait for the next frame
        }

        // Ensure the object reaches the exact target position
        transform.position = target.position;

        isMoving = false; // Movement is completed
    }

    private void shoot()
    {
        // Check if hit target
        // Check if the circle is touching the target
        if (Vector2.Distance(transform.position, target.position) <= touchRadius)
        {
            target.GetComponent<playerHealth>().GetHit(1);
        }
        // shake camera
        camera.ShakeCamera();
        // Destroy self
        Destroy(gameObject);
    }
}
