using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; 
    public Transform spawnPoint;   
    public float spawnInterval = 2f; 
    public float obstacleSpeed = 5f; 

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (obstacles.Length > 0)
            {
                int randomIndex = Random.Range(0, obstacles.Length);
                float randomYPosition = Random.Range(-1f, 1f);
                GameObject spawnedObstacle = Instantiate(obstacles[randomIndex], spawnPoint.position + new Vector3(0, randomYPosition, 0), Quaternion.identity);
                spawnedObstacle.AddComponent<ObstacleMover>().speed = obstacleSpeed;
            }
        }
    }
}