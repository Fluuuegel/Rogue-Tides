using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [System.Serializable]
public class EnemyStats
{
    public float hp;
    public float speed;
}
public EnemyStats[] enemyStats;
    public GameObject enemyPrefab;
    public Transform island;
    public Transform playerShip; // <- add this
    public float spawnInterval = 3f;
    public Sprite[] enemySprites;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    void SpawnEnemy()
{
    float distance = Random.Range(6f, 6f);          // distance from player
    float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;  // random direction

    float xOffset = Mathf.Cos(angle) * distance;
    float yOffset = Mathf.Sin(angle) * distance;

    Vector3 spawnPos = playerShip.position + new Vector3(xOffset, yOffset, 0f);

    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        EnemyBoat boat = enemy.GetComponent<EnemyBoat>();
        boat.target = island;

        int index = Random.Range(0, enemySprites.Length);

         // Assign sprite
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        sr.sprite = enemySprites[index];

        // Assign matching stats
        boat.hp = enemyStats[index].hp;
        boat.speed = enemyStats[index].speed;
}
}

