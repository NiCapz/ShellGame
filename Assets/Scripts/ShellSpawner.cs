using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private float minDistance;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    List<int> unavailableSpawnPoints = new List<int>();
    List<GameObject> shells = new List<GameObject>();
    Transform[] blanketSpawnPoints;
    HashSet<Shell.ShellType> foundShellTypes = new HashSet<Shell.ShellType>();

    public void SpawnShellOnBlanket(Shell.ShellType type)
    {
        if (!foundShellTypes.Contains(type))
        {
            foundShellTypes.Add(type);
            Instantiate(shellPrefab, blanketSpawnPoints[(int)type].position, Quaternion.identity);
        }
    }

    void Awake()
    {
        blanketSpawnPoints = GameObject.Find("BlanketShellLocations").GetComponentsInChildren<Transform>();
    }

    public void SpawnShells()
    {
        shells.Clear();
        int shellsToSpawn = Random.Range(2, 4);

        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < shellsToSpawn; i++)
        {
            int spawnPointIndex;
            bool spawnPointFound = false;
            do
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Count);
                if (!unavailableSpawnPoints.Contains(spawnPointIndex))
                {
                    spawnPointFound = true;
                    unavailableSpawnPoints.Add(spawnPointIndex);
                }
            } while (!spawnPointFound);
            spawnPosition = spawnPoints[spawnPointIndex].position;
            spawnPosition.z = Camera.main.nearClipPlane + 1f;

            shells.Add(Instantiate(shellPrefab, spawnPosition, Quaternion.identity));
        }
    }

    public void DespawnShells()
    {
        foreach (GameObject shell in shells)
        {
            Destroy(shell);
        }
        shells.Clear();
    }
}