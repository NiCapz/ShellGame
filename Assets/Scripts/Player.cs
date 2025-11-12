using UnityEngine;

public class Player : MonoBehaviour
{

    private Transform seaShell;
    [SerializeField] private GameObject seaShellPrefab;
    public Vector2 mouseposition;
    [SerializeField] private ShellSpawner shellSpawner;

    void Start()
    {
        //shellSpawner = GetComponent<ShellSpawner>();
        seaShell = GameObject.Find("Shell").transform;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z; 

        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(worldMousePos);
        mouseposition = worldMousePos;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shellSpawner.SpawnShells();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            shellSpawner.DespawnShells();
        }
    }
}
