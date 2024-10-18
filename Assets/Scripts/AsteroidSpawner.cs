using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Array to hold different asteroid prefabs
    [SerializeField] GameObject[] astPrefab = new GameObject[3];

    // Time interval between asteroid spawns
    public float spawnRate = 1f;

    // Timer to track time passed since last spawn
    float timeInSecs = 0f;

    // Update is called once per frame
    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timeInSecs += Time.deltaTime;

        // Check if the timer has reached the spawn rate
        if (timeInSecs >= spawnRate)
        {
            // Spawn a new asteroid
            SpawnAsteroid();

            // Reset the timer
            timeInSecs = 0.0f;
        }               
    }

    // Method to spawn a new asteroid
    void SpawnAsteroid()
    {
        // Select a random asteroid prefab from the array
        GameObject selectedObj = astPrefab[Random.Range(0, astPrefab.Length)];

        // Get a random position to spawn the asteroid
        Vector2 spawnPos = GetRandomSpawnPosition();

        // Instantiate the asteroid at the random position with no rotation
        Instantiate(selectedObj, spawnPos, Quaternion.identity);
    }

    // Method to get a random position on the screen edges for spawning the asteroid
    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPos = Vector2.zero;
        float randVal = Random.value;

        // Determine the spawn position based on a random value
        if (randVal < 0.25f) // Top edge
        {
            spawnPos = new Vector2(Random.Range(-CameraBounds.screenBounds.x, CameraBounds.screenBounds.x), CameraBounds.screenBounds.y);
        }
        else if (randVal < 0.50f) // Right edge
        {
            spawnPos = new Vector2(CameraBounds.screenBounds.x, Random.Range(-CameraBounds.screenBounds.y, CameraBounds.screenBounds.y));
        }
        else if (randVal < 0.75f) // Bottom edge
        {
            spawnPos = new Vector2(Random.Range(-CameraBounds.screenBounds.x, CameraBounds.screenBounds.x), -CameraBounds.screenBounds.y);
        }
        else // Left edge
        {
            spawnPos = new Vector2(-CameraBounds.screenBounds.x, Random.Range(-CameraBounds.screenBounds.y, CameraBounds.screenBounds.y));
        }

        return spawnPos;
    }
}