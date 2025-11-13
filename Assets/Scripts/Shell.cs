using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IClickable
{
    public enum ShellType
    {
        Scallop = 0,
        Cowrie  = 1,
        Murex   = 2,
    }
    private static ShellType[] drawTable = { ShellType.Scallop, ShellType.Cowrie, ShellType.Murex };
    

    [SerializeField] SpriteRenderer outline;

    private ShellType shellType;
    private AssetProvider assetProvider;
    private ShellSpawner shellSpawner;

    void Start()
    {
        assetProvider = FindAnyObjectByType<AssetProvider>();
        shellSpawner = FindAnyObjectByType<ShellSpawner>();

        int random = Random.Range(0, 3);
        shellType = drawTable[random];

        int spriteIndex = (int) shellType;
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
        Player.RemoveFromClickables(gameObject);
        Destroy(gameObject);
        shellSpawner.SpawnShellOnBlanket(shellType);
    }
}
