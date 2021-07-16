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

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Spawn the objects
        StartCoroutine(SpawnObjects());        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // SpawnCollectible();
            yield return new WaitForSeconds(gameManager.timeBetweenObjectSpawns / 2.0f);
            SpawnSimpleObstacle();
            yield return new WaitForSeconds(gameManager.timeBetweenObjectSpawns / 2.0f);
        }
    }

}
