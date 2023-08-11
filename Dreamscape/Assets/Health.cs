using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoints;
    public int currentHealth;
    void Start()
    {
        currentHealth = healthPoints;
    }


    public void RestoreHealth(int heal)
    {
        currentHealth += heal;
        Mathf.Clamp(currentHealth, 0, 100);
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
