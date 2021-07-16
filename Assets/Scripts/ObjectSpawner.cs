using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private GameManager gameManager;

    // Prefab variables
    public GameObject bitPrefab;
    public GameObject[] simpleObstaclePrefabs;
    public GameObject[] complexObstaclePrefabs;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Spawn the objects
        StartCoroutine(SpawnObjects());        
    }

    void SpawnBit()
    {
        // Randomly determine where to spawn the bit
        BitMove bitMove = bitPrefab.GetComponent<BitMove>();
        float spawnY = Random.Range(bitMove.minSpawnY, bitMove.maxSpawnY);
        Vector3 spawnPos = new Vector3(gameManager.objectSpawnX, spawnY, 0.0f);

        // Spawn the bit
        Instantiate(bitPrefab, spawnPos, Quaternion.identity);
    }

    void SpawnSimpleObstacle()
    {
        // Pick a random simple obstacle to spawn
        int randomIndex = Random.Range(0, simpleObstaclePrefabs.Length);
        GameObject obstaclePrefab = simpleObstaclePrefabs[randomIndex];

        // Determine the spawn position of the obstacle
        float spawnX = gameManager.objectSpawnX;
        float spawnY = obstaclePrefab.GetComponent<ObstacleMove>().spawnY;
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0.0f);

        // Spawn the obstacle
        GameObject obstacleInstance = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // If the obstacle is not the largest variation of obstacle,
        // randomly determine whether it should also move vertically
        if (obstacleInstance.transform.localScale.y < 8.0f)
        {
            bool doesMoveVertically = Random.Range(0, 3) == 0;
            obstacleInstance.GetComponent<ObstacleMove>().doesMoveVertically = doesMoveVertically;
        }
    }

    void SpawnComplexObstacle()
    {
        // Pick a random complex obstacle to spawn
        int randomIndex = Random.Range(0, complexObstaclePrefabs.Length);
        GameObject obstaclePrefab = complexObstaclePrefabs[randomIndex];

        // Spawn the obstacle
        Vector3 spawnPos = new Vector3(gameManager.objectSpawnX, 0.0f, 0.0f);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    void SpawnObstacle()
    {
        // Randomly determine whether to spawn a simple or complex obstacle
        bool doSpawnSimpleObstacle = Random.Range(0, 3) != 0;

        // Spawn the proper obstacle
        if (doSpawnSimpleObstacle)
            SpawnSimpleObstacle();
        else
            SpawnComplexObstacle();
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnBit();
            yield return new WaitForSeconds(gameManager.timeBetweenObjectSpawns / 2.0f);
            SpawnObstacle();
            yield return new WaitForSeconds(gameManager.timeBetweenObjectSpawns / 2.0f);
        }
    }

}
