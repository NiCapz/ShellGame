using UnityEngine;

public class JournalButton : MonoBehaviour, IClickable
{
   
    JournalManager journalManager;
    [SerializeField] SpriteRenderer outline;


    void Awake()
    {
        journalManager = FindAnyObjectByType<JournalManager>();
    }

    public void ClickOn()
    {
        journalManager.ToggleJournal();
        Debug.Log("clicked on journal");
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
