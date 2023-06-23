using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerate : MonoBehaviour
{
    public int width;   // Width of the maze
    public int height;  // Height of the maze
    public float cellWidth; // Width of the cell
    public float cellHeight; // Height of the cell
    public GameObject weakWallPrefab;
    public GameObject strongWallPrefab;
    public GameObject floorPrefab;
    public GameObject boundaryPrefab;

    [Range(0, 1)]
    public float rateOfWeakWalls;

    bool[,] maze;  // 2D array to store the maze structure

    public void GenerateMaze()
    {
        maze = new bool[width, height];  // Initialize the maze structure

        // Set all cells as walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = true;
            }
        }

        Vector2Int currentCell = Vector2Int.zero;  // Start from the top-left corner
        maze[currentCell.x, currentCell.y] = true;  // Mark the starting cell as a path

        // Generate the maze using a depth-first search algorithm
        GenerateMazeRecursive(currentCell);

        // Temporary object for set parent transform
        GameObject tmp;

        // Spawn floor
        tmp = Instantiate(floorPrefab, new Vector3(width / 2, height / 2, 0), Quaternion.identity);
        tmp.transform.localScale = new Vector3(width * cellWidth + 2, height * cellHeight + 2, 1);
        tmp.transform.parent = transform;

        // Display the generated maze (you can modify this part according to your needs)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y])
                {
                    // Spawn a wall object at position (x, y) and set as child of generator
                    tmp = Instantiate(GetWallByChance(rateOfWeakWalls),
                    new Vector3(x * cellWidth, y * cellHeight, 0), Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }
        SetBoundaries();
    }

    void GenerateMazeRecursive(Vector2Int cell)
    {
        // Define the four possible directions to move in the maze
        Vector2Int[] directions = {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        // Randomly shuffle the directions
        Shuffle(directions);

        // Iterate over each direction
        for (int i = 0; i < directions.Length; i++)
        {
            // Calculate the next cell's coordinates
            Vector2Int nextCell = cell + (directions[i] * 2);  // Move 2 steps

            // Check if the next cell is within the maze bounds
            if (IsCellValid(nextCell))
            {
                // Check if the next cell is a wall
                if (maze[nextCell.x, nextCell.y])
                {
                    // Mark the next cell as a path
                    maze[nextCell.x, nextCell.y] = false;

                    // Mark the cell between the current cell and the next cell as a path
                    maze[cell.x + directions[i].x, cell.y + directions[i].y] = false;

                    // Recursively call the function with the next cell
                    GenerateMazeRecursive(nextCell);
                }
            }
        }
    }

    bool IsCellValid(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < width && cell.y >= 0 && cell.y < height;
    }

    void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    GameObject GetWallByChance(float chanse)
    {
        float randFloat = Random.Range(0f, 1f);

        return randFloat > chanse ? strongWallPrefab : weakWallPrefab;
    }

    /// <summary>
    /// Set boundairies around the perimeter of labyrynth
    /// </summary>
    void SetBoundaries()
    {
        Vector3 midle = new Vector3(width * cellWidth / 2, height * cellHeight / 2, 0);
        Vector3 size = new Vector3(1, height * cellHeight + 3, 1);
        Vector3 offset = new Vector3(0, height * cellHeight / 2 + 1, 0);

        GameObject tmp;

        for (int i = 0; i < 4; i++)
        {
            offset = Quaternion.Euler(0, 0, 90) * offset;

            tmp = Instantiate(boundaryPrefab, midle + offset, Quaternion.identity);
            tmp.transform.parent = transform;
            tmp.transform.localScale = size;
            tmp.transform.localEulerAngles = new Vector3(0, 0, 90 * i);
        }
    }
}

