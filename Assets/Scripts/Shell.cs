using UnityEngine;

public class Shell : MonoBehaviour, IClickable
{
    public enum ShellType
    {
        Scallop = 0,
        Murex = 1,
        Cowrie = 2,
        Conch = 3,
        Nautilus = 4,
        BandedWedge = 5,
        Starfish = 6,
        Turret = 7,
        Periwinkle = 8,
        Pearl = 9
    }

    private static ShellType[] drawTable = { ShellType.Scallop, ShellType.Murex, ShellType.Cowrie, ShellType.Conch,
     ShellType.Nautilus, ShellType.BandedWedge, ShellType.Starfish, ShellType.Turret, ShellType.Periwinkle };


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
        //int random = Random.Range(0, 9);
        //shellType = drawTable[random];

        int random = Random.Range(0, 100);
        ShellType type = ShellType.Periwinkle;
        switch (random)
        {
            case < 16:
                type = ShellType.Periwinkle;
                break;
            case < 32:
                type = ShellType.Scallop;
                break;
            case < 48:
                type = ShellType.Starfish;
                break;
            case < 60:
                type = ShellType.Turret;
                break;
            case < 72:
                type = ShellType.BandedWedge;
                break;
            case < 84:
                type = ShellType.Cowrie;
                break;
            case < 89:
                type = ShellType.Conch;
                break;
            case < 94:
                type = ShellType.Murex;
                break;
            case < 99:
                type = ShellType.Nautilus;
                break;
            case < 100:
                type = ShellType.Pearl;
                break;

        }
        shellType = type;
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
            ShellSpawner.shells.Remove(gameObject);
            if (ShellSpawner.shells.Count <= 0)
            {
                FindAnyObjectByType<Player>().StartWave();
            }
            Destroy(gameObject);
        }
    }
}
