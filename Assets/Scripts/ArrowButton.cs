using UnityEngine;

public class ArrowButton : MonoBehaviour, IClickable
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] CameraAnimation cameraAnimation;

    static bool atBeach = true;

    void Awake()
    {
        outline.enabled = false;

    }

    public void ClickOn()
    {
        Debug.Log("clicked on arrow");
         if (atBeach)
            {
                cameraAnimation.EaseUp();
            } else
            {
                cameraAnimation.EaseDown();
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
