using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [Header("Round")]
    public int round;
    public float roundTime;
    public float timer;
    public int remainingEnemies;


    [Header("UI")]
    public Image imageUI; // Reference to the Image component
    public Sprite[] weaponsSprites; // Reference to the Sprite you want to assign

    [Header("Player")]
    public Player player;


    [Header("Enemies")]
    public GameObject[] Enemies;

    void Start()
    {
        round = 1;
        getNewWeapon();
        timer = roundTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        // Increase round and reset timer.
        round += 1;
        timer = roundTime;

        // Kill all enemies

        // Increase the difficulity based on round number

        // Randomise player weapon
        getNewWeapon();
    }

    private void KillAll()
    {

    }

    private void getNewWeapon()
    {
        int n = 2;
        int randomNumber = Random.Range(0, n + 1);
        player.switchWeapon(randomNumber);
        imageUI.sprite = weaponsSprites[randomNumber]; // Assign the new sprite to the Image component
    }

    private void spwanEnemy()
    {
        int n = 100;
        int randomNumber = Random.Range(0, n + 1);
        if(randomNumber < 40)
        {

        }else if(randomNumber < 70)
        {

        }else if (randomNumber < 90)
        {

        }
        else if (randomNumber < 10) { 
        }

    }
}
