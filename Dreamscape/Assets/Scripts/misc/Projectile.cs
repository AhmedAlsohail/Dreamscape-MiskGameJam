using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public GameObject DestroyEffect;
    private void Start()
    {
        Invoke("DestroyPorjectile", lifetime);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("we hit");
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void DestroyPorjectile()
    {
        Destroy(gameObject);
        if (DestroyEffect)
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
    }
}