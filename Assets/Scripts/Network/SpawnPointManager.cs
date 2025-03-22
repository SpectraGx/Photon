using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private static SpawnPointManager instance;
    public static SpawnPointManager Instance { get { return instance; } }

    public List<Transform> spawnPoints;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No hay spawn points configurados!");
            return Vector3.zero;
        }
        
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].position;
    }
}
