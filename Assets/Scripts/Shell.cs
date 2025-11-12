using UnityEngine;

public class Shell : MonoBehaviour, IClickable
{
    [SerializeField] Renderer outline;

    void Start()
    {
        outline.enabled = false;
    }

    void OnMouseEnter()
    {
        Debug.Log("shell entered");
        outline.enabled = true;
        Player.AddToClickables(gameObject);
    }
    
    void OnMouseExit()
    {
        outline.enabled = false;
        Debug.Log("Shell exited");
        Player.RemoveFromClickables(gameObject);
    }

    public void ClickOn()
    {
        Player.RemoveFromClickables(gameObject);
        Destroy(gameObject);
    }
}
