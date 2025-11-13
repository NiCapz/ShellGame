using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

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

    void Awake()
    {
        assetProvider = FindAnyObjectByType<AssetProvider>();
        Debug.Log(assetProvider.enabled);
        shellSpawner = FindAnyObjectByType<ShellSpawner>();
        SetRandomType();
        SetSprite();
    }

    public void SetSpecificType(ShellType type)
    {
        shellType = type;
        transform.localScale = new Vector3(2f, 2f, 2f);
        
    }

    public void SetRandomType()
    {
        int random = Random.Range(0, 3);
        shellType = drawTable[random];
    }

    public void SetSprite()
    {
        int spriteIndex = (int) shellType;
        Debug.Log(assetProvider.enabled);
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
        shellSpawner.SpawnShellOnBlanket(shellType);
        Destroy(gameObject);
    }
}
