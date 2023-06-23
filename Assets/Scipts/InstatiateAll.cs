using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class InstatiateAll : MonoBehaviour
{
    public MazeGenerate maze;
    public int enemyCount;
    public float timeInterval;
    public GameObject enemyPrefab;
    public GameObject basePrefab;

    int enemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = enemyCount;

        // Spawn maze
        maze.GenerateMaze();
        AstarPath.active.Scan();

        // Spawn base near in midle bottom side
        Instantiate(basePrefab, 
        GetSpawnPoint(maze.width / 3, 2 * maze.width / 3, 0, maze.height / 3), 
        Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }

    /// <summary>
    /// Find point without any colliders
    /// </summary>
    Vector3 GetSpawnPoint(int xMin, int xMax, int yMin, int yMax)
    {
        Vector3 randomPoint;

        while (true)
        {
            randomPoint = new Vector3Int(
                Random.Range(xMin, xMax + 1),
                Random.Range(yMin, yMax + 1),
                0
            );

            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPoint, 0.1f);

            if (colliders.Length == 0)
            {
                break;
            }
        }

        return randomPoint;
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyToSpawn > 0)
        {
            // Spawn enemy in half of a labyrythm
            Instantiate(enemyPrefab, 
            GetSpawnPoint(0, maze.width, maze.height / 2, maze.height), 
            Quaternion.identity);
            enemyToSpawn--;
            yield return new WaitForSeconds(timeInterval);
        }
        
    }

}