using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{

    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private float minDistance;
    List<GameObject> shells = new List<GameObject>();

    private Vector4 spawnArea;

    void Start()
    {
        spawnArea = DefineSpawnArea();
    }

    private Vector4 DefineSpawnArea()
    {
        float leftBorder = Screen.width / 10;
        float rightBorder = Screen.width - Screen.width / 10;
        float topBorder = Screen.height - Screen.height / 3;
        float lowerBorder = Screen.height / 2;

        return new Vector4(leftBorder, rightBorder, topBorder, lowerBorder);
    }


    public void SpawnShells()
    {
        shells.Clear();
        int shellsToSpawn = Random.Range(2, 4);

        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < shellsToSpawn; i++)
        {
            bool spawnPositionFound = true;
            int attemptCounter = 1;
            do
            {
                spawnPosition.x = Random.Range(spawnArea.x, spawnArea.y);
                spawnPosition.y = Random.Range(spawnArea.z, spawnArea.w);
                spawnPosition.z = Camera.main.nearClipPlane + 1f;

                if (shells.Count > 0)
                {
                    foreach (GameObject shell in shells)
                    {
                        float distanceToShell = Vector2.Distance(shell.transform.position, spawnPosition);
                        if (distanceToShell < minDistance)
                        {
                            spawnPositionFound = false;
                            attemptCounter++;
                            break;
                        } else
                        {
                            spawnPositionFound = true;
                        }
                    }
                }
            } while (!spawnPositionFound && attemptCounter < 20);

            spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

            shells.Add(Instantiate(shellPrefab, spawnPosition, Quaternion.identity));

        }
    }

    public void DespawnShells()
    {
        foreach (GameObject shell in shells)
        {
            GameObject.Destroy(shell);
        }
        shells.Clear();
    }
}