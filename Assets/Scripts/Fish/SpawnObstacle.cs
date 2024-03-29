using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float spanwTimeDecrease;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float obstacleToSpawn;
    [SerializeField] private float obstacleToSpawnIncrease;
    [SerializeField] private float maxObstacleToSpawn;



    public static bool stop = false;
    void Start()
    {
        Invoke(nameof(StartSpawn), 3);
    }
    void StartSpawn()
    {
        StartCoroutine(nameof(SpawnNewObstacle));
    }

    IEnumerator SpawnNewObstacle()
    {
        if(obstacleToSpawn > maxObstacleToSpawn)
        {
            obstacleToSpawn = maxObstacleToSpawn;
        }
        for (int i = Mathf.RoundToInt(obstacleToSpawn); i > 0; i--){

            float y = Mathf.Round(Random.Range(0, spawnRange.y));
            float x = Random.Range(0, spawnRange.x);

            Vector2 spawnPosition = new Vector2(transform.position.x + x, transform.position.y + y);

            Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(timeToSpawn);
        if(obstacleToSpawn >= 1)
        {
            obstacleToSpawn += obstacleToSpawnIncrease;
        }
        
        if (timeToSpawn > minTimeToSpawn && timeToSpawn < maxTimeToSpawn)
            timeToSpawn += spanwTimeDecrease;
        
        if (!stop)
            StartCoroutine(nameof(SpawnNewObstacle));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + spawnRange.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + spawnRange.x, transform.position.y));

    }
}
