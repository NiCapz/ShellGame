using UnityEngine;

public class JournalButton : MonoBehaviour, IClickable
{

    JournalManager journalManager;
    [SerializeField] SpriteRenderer outline;
    [SerializeField] Diary diary;


    void Awake()
    {
        journalManager = FindAnyObjectByType<JournalManager>();
        outline.enabled = false;

    }

    public void ClickOn()
    {
        journalManager.ToggleJournal();
        diary.DoPagination();
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
