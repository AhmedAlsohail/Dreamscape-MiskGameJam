using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int healthPoints;
    public int currentHealth;
    public GameObject deathPanel;
    public GameObject roundManager;
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Apply particle effect
        deathPanel.SetActive(true);
        Destroy(roundManager);
        gameObject.SetActive(false);
    }
}
