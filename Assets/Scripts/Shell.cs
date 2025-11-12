using UnityEngine;

public class Shell : MonoBehaviour, IClickable
{
    public enum ShellType
    {
        Scallop = 0,
        Iridescent= 1
    }
    private static ShellType[] drawTable = { ShellType.Scallop, ShellType.Iridescent };

    [SerializeField] SpriteRenderer outline;

    private ShellType shellType;
    private AssetProvider assetProvider;

    void Start()
    {
        assetProvider = GameObject.FindAnyObjectByType<AssetProvider>();

        int random = Random.Range(0, 2);
        shellType = drawTable[random];
        Debug.Log(random);


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
    }
}
