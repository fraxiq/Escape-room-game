using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] bikeSpawnpoints;
    public GameObject[] plant1Spawnpoints;
    public GameObject[] plant2Spawnpoints;
    public GameObject[] strollerSpawnpoints;
    public GameObject[] suitcasesSpawnpoints;
    public GameObject[] boxesSpawnpoints;

    void Start()
    {
        GenerateLevel();
    }

    Vector3 GenerateSpawn(int objectType)
    {
        GameObject[] spawnpoints = null;
        switch (objectType)
        {
            case 0:
                spawnpoints = bikeSpawnpoints;
                break;
            case 1:
                spawnpoints = boxesSpawnpoints;
                break;
            case 2:
                spawnpoints = plant1Spawnpoints;
                break;
            case 3:
                spawnpoints = plant2Spawnpoints;
                break;
            case 4:
                spawnpoints = strollerSpawnpoints;
                break;
            case 5:
                spawnpoints = suitcasesSpawnpoints;
                break;
            default:
                Debug.LogError("Invalid object index!");
                break;
        }

        if (spawnpoints != null)
        {
            int maxAttempts = 50; // Избягване на infinte loop
            int attempt = 0;
            Vector3 spawnPosition = Vector3.zero;
            while (attempt < maxAttempts)
            {
                int index = Random.Range(0, spawnpoints.Length);
                spawnPosition = spawnpoints[index].transform.position;
                GameObject newObj = Instantiate(objects[objectType], spawnPosition, Quaternion.identity);
                if (!CheckCollision(newObj))
                {
                    
                    return spawnPosition;
                }
                
                Destroy(newObj);
                attempt++;
            }
        }

        Debug.LogError("Failed to find a non-colliding spawn point!");
        return Vector3.zero;
    }

    void GenerateLevel()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 spawnPosition = GenerateSpawn(i);
            Debug.Log("Spawned object " + i + " at position: " + spawnPosition);
        }
    }

    bool CheckCollision(GameObject obj)
    {
        
        Collider2D objCollider = obj.GetComponent<Collider2D>();
     
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        
        foreach (Collider2D collider in allColliders)
        {
            
            if (collider.gameObject != obj &&
                !collider.CompareTag("SpawnPoint") &&
                !collider.CompareTag("Wall") &&
                !collider.CompareTag("Player"))
            {
                
                if (objCollider.IsTouching(collider))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
