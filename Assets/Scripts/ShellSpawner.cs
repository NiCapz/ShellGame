using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{

    [SerializeField] private GameObject shellPrefab;
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
        float topBorder = Screen.height / 2;
        float lowerBorder = Screen.height / 10;

        return new Vector4(leftBorder, rightBorder, topBorder, lowerBorder);
    }

    public void SpawnShells()
    {
        int shellsToSpawn = Random.Range(2, 6);

        for (int i = 0; i < shellsToSpawn; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(spawnArea.x, spawnArea.y),
                Random.Range(spawnArea.z, spawnArea.w),
                Camera.main.nearClipPlane + 1f);

            spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
            shells.Add(Instantiate(shellPrefab, spawnPosition, Quaternion.identity));
            Debug.Log($"Spawned shell at {spawnPosition}");
        }
    }
    
    public void DespawnShells()
    {
        foreach(GameObject shell in shells)
        {
            GameObject.Destroy(shell);
        }
    }
}
