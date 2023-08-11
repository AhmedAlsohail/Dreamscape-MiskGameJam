using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    [Header("Round")]
    public int round;
    public float roundTime;
    public float timer;
    public int remainingEnemies;


    public int nOfEnemies;
    public int currentEnemies;
    public float spawnEachInSeconds;
    public float spawnTimer;
    private float nOfEnemiesMultiplayer = 2f;

    public Transform[] drakeSpawnPoints;
    public Transform[] jackalsSpawnPoints;
    public Transform[] ghoulSpawnPoints;
    public Transform[] fiendsSpawnPoints;

    [Header("UI")]
    public Image imageUI; // Reference to the Image component
    public Sprite[] weaponsSprites; // Reference to the Sprite you want to assign
    public TextMeshProUGUI health;

    [Header("Player")]
    public Player player;


    [Header("Enemies")]
    public GameObject[] Enemies;

    void Start()
    {
        round = 1;
        getNewWeapon();

        timer = roundTime;
        nOfEnemies = Mathf.CeilToInt(round * nOfEnemiesMultiplayer) + 4;
        currentEnemies = nOfEnemies;

        spawnEachInSeconds = roundTime / nOfEnemies;
        spawnTimer = spawnEachInSeconds;
        spwanEnemy();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            spwanEnemy();
            spawnTimer = spawnEachInSeconds;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            StartNewRound();
        }

        // Update UI (Not best approach but I dont have time)
        health.text = player.GetComponent<playerHealth>().currentHealth.ToString();

    }

    private void StartNewRound()
    {
        // Increase round and reset timer.
        round += 1;
        timer = roundTime;

        // Kill all enemies

        // Increase the difficulity based on round number
        nOfEnemies = (int)(round * nOfEnemiesMultiplayer);
        currentEnemies = nOfEnemies;
        spawnEachInSeconds = roundTime / nOfEnemies;

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
        //imageUI.sprite = weaponsSprites[randomNumber]; // Assign the new sprite to the Image component
        spawnTimer = spawnEachInSeconds;
    }

    private void spwanEnemy()
    {
        Debug.Log("Sapwned");
        int n = 100;
        int randomNumber = Random.Range(0, n + 1);
        int spawnPoint = 0;
        if(randomNumber < 40) // drakes
        {            
            spawnPoint = Random.Range(0, drakeSpawnPoints.Length);
            Instantiate(Enemies[0], drakeSpawnPoints[spawnPoint].position, Quaternion.identity);
        }
        else if(randomNumber < 70) // jacals
        {
            spawnPoint = Random.Range(0, jackalsSpawnPoints.Length);
            Instantiate(Enemies[1], jackalsSpawnPoints[spawnPoint].position, Quaternion.identity);

        }
        else if (randomNumber < 80) // ghoul
        {
            spawnPoint = Random.Range(0, ghoulSpawnPoints.Length);
            Instantiate(Enemies[2], ghoulSpawnPoints[spawnPoint].position, Quaternion.identity);

        }
        else if (randomNumber < 90) // fiends
        {
            spawnPoint = Random.Range(0, fiendsSpawnPoints.Length);
            Instantiate(Enemies[3], fiendsSpawnPoints[spawnPoint].position, Quaternion.identity);

        }
        else // Imposter Sniper
        {
            Instantiate(Enemies[4], Vector3.zero, Quaternion.identity);
        }
    }
}
