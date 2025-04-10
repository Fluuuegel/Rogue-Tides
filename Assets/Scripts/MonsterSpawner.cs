using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject BossMonster;

    public Tilemap islandBounds;
    public LayerMask obstacleLayer;

    void Start() {
        SpawnMonsters(); // spawn monsters at the start of the game
    }

    void SpawnMonsters() {
        //Vector3 randomPosition = Random.insideUnitCircle * Radius; // edit this line
        //Instantiate(Monster1, randomPosition, Quaternion.identity);
    }
}
