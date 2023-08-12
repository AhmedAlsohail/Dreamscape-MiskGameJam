using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int healthPoints;
    public int currentHealth;
    public GameObject deathPanel;
    public GameObject roundManager;
    public GameObject Timer;
    public GameObject currentWeapon;
    public GameObject health;

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
        Timer.SetActive(false);
        currentWeapon.SetActive(false);
        health.SetActive(false);

        // Hide rest of UIs
        Destroy(roundManager);
        gameObject.SetActive(false);

    }
}
