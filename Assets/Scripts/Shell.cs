using UnityEngine;

public class Shell : MonoBehaviour, IClickable
{
    public enum ShellType
    {
        Scallop     = 0,
        Murex       = 1,
        Cowrie      = 2,
        Conch       = 3,
        Nautilus    = 4,
        RazorClam   = 5,
        Starfish    = 6,
        Turret      = 7,
        Periwinkle  = 8
    }

    private static ShellType[] drawTable = { ShellType.Scallop, ShellType.Murex, ShellType.Cowrie, ShellType.Conch,
     ShellType.Nautilus, ShellType.RazorClam, ShellType.Starfish, ShellType.Turret, ShellType.Periwinkle };


    [SerializeField] SpriteRenderer outline;

    private ShellType shellType;
    private AssetProvider assetProvider;
    private ShellSpawner shellSpawner;
    private bool clickable = true;

    void Awake()
    {
        assetProvider = FindAnyObjectByType<AssetProvider>();
        shellSpawner = FindAnyObjectByType<ShellSpawner>();
        SetRandomType();
        SetSprite();
    }

    public void SetSpecificType(ShellType type)
    {
        shellType = type;
        transform.localScale = new Vector3(3f, 3f, 3f);
        clickable = false;
    }

    public void SetRandomType()
    {
        int random = Random.Range(0, 9);
        shellType = drawTable[random];
    }

    public void SetSprite()
    {
        int spriteIndex = (int)shellType;
        Sprite sprite = assetProvider.shellSprites[spriteIndex];
        Sprite outlineSprite = assetProvider.shellSpritesOutline[spriteIndex];
        GetComponent<SpriteRenderer>().sprite = sprite;
        outline.sprite = outlineSprite;

        outline.enabled = false;
    }

    void OnMouseEnter()
    {
        outline.enabled = true;
        Player.AddToClickables(gameObject);
    }

    void OnMouseExit()
    {
        outline.enabled = false;
        Player.RemoveFromClickables(gameObject);
    }

    public void ClickOn()
    {
        if (clickable)
        {
            Player.RemoveFromClickables(gameObject);
            shellSpawner.SpawnShellOnBlanket(shellType);
            FindAnyObjectByType<JournalManager>().ShellFound(shellType);
            Destroy(gameObject);
        }
    }
}
