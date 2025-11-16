using DigitalRuby.Tween;
using UnityEngine;

public class ArrowButton : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] CameraAnimation cameraAnimation;

    private SpriteRenderer spriteRenderer;
    private AssetProvider assetProvider;
    private Vector3 startingPos;
    private Vector3 endPos;
    static bool atBeach = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        assetProvider = FindAnyObjectByType<AssetProvider>();
        outline.enabled = false;
        startingPos = transform.position;
        endPos = new Vector3(7.7f, 5.31f, startingPos.z);

    }

    private void SetRedSprite()
    {
        spriteRenderer.sprite = assetProvider.arrowSprites[0];
        outline.sprite = assetProvider.arrowOutlineSprites[0];
    }

    private void SetBlueSprite()
    {
        spriteRenderer.sprite = assetProvider.arrowSprites[1];
        outline.sprite = assetProvider.arrowOutlineSprites[1];
    }

    private void EaseUp()
    {
        TweenFactory.Tween(null, startingPos, endPos, .5f, TweenScaleFunctions.CubicEaseInOut, t => transform.position = t.CurrentValue, t => Debug.Log("eased up arrow"));
        TweenFactory.Tween(
        "rotateUp",
        0f, 180f,
        0.5f, TweenScaleFunctions.CubicEaseInOut,
        t =>
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = t.CurrentValue;
            transform.eulerAngles = rot;
            SetBlueSprite();
        }
    );
    }
    private void EaseDown()
    {
        TweenFactory.Tween(null, endPos, startingPos, .5f, TweenScaleFunctions.CubicEaseInOut, t => transform.position = t.CurrentValue, t => Debug.Log("eased down arrow"));
        TweenFactory.Tween(
        "rotateDown",
        180f, 0f,
        0.5f, TweenScaleFunctions.CubicEaseInOut,
        t =>
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = t.CurrentValue;
            transform.eulerAngles = rot;
            SetRedSprite();
        }
    );
    }

    public void ClickOn()
    {
        Debug.Log("clicked on arrow");
        if (atBeach)
        {
            cameraAnimation.EaseUp();
            EaseUp();
        }
        else
        {
            cameraAnimation.EaseDown();
            EaseDown();
        }
        atBeach = !atBeach;
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
}
