using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private float minDistance;
    Transform[] spawnPoints;
    List<int> unavailableSpawnPoints = new List<int>();
    List<GameObject> shells = new List<GameObject>();
    Transform[] blanketSpawnPoints;
    HashSet<Shell.ShellType> foundShellTypes = new HashSet<Shell.ShellType>();

    public void SpawnShellOnBlanket(Shell.ShellType type)
    {
        if (!foundShellTypes.Contains(type))
        {
            foundShellTypes.Add(type);
            int spawnPointIndex = (int)type + 1;
            Vector3 spawnPosition = blanketSpawnPoints[spawnPointIndex].position;
            spawnPosition.z = Camera.main.nearClipPlane + 1f;

            Shell shell = Instantiate(shellPrefab, spawnPosition, Quaternion.identity).GetComponent<Shell>();
            shell.SetSpecificType(type);
            shell.SetSprite();
            
            Destroy(shell.GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    void Awake()
    {
        spawnPoints = GameObject.Find("ShellSpawnPoints").GetComponentsInChildren<Transform>();
        blanketSpawnPoints = GameObject.Find("BlanketShellLocations").GetComponentsInChildren<Transform>();
    }

    public void SpawnShells()
    {
        shells.Clear();
        int shellsToSpawn = Random.Range(2, 4);

        for (int i = 0; i < shellsToSpawn; i++)
        {
            int spawnPointIndex;
            bool spawnPointFound = false;
            do
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Length);
                if (!unavailableSpawnPoints.Contains(spawnPointIndex))
                {
                    spawnPointFound = true;
                    unavailableSpawnPoints.Add(spawnPointIndex);
                }
            } while (!spawnPointFound);
            Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
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