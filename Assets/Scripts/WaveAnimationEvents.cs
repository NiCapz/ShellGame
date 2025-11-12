using UnityEngine;

public class WaveAnimationEvents : MonoBehaviour
{
    [SerializeField] private ShellSpawner shellSpawner;


    private void SpawnShells()
    {
        shellSpawner.SpawnShells();
    }
}
