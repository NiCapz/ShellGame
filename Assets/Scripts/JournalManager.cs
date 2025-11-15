using System.Linq;
using TMPro;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public enum Tabs { Diary, Shells, Options }

    [SerializeField] private GameObject allJournals;
    [SerializeField] private GameObject journalOptions;
    [SerializeField] private GameObject journalDiary;
    [SerializeField] private GameObject journalShells;

    [SerializeField] private GameObject[] checkMarks;
    [SerializeField] private TextMeshProUGUI[] amountsGuis;

    private Diary diary;
    private int[] amounts = new int[9];

    void Awake()
    {
        diary = FindAnyObjectByType<Diary>();
        foreach (GameObject checkmark in checkMarks)
        {
            checkmark.SetActive(false);
        }
        ToggleJournal();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.B)) && allJournals.activeInHierarchy)
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        bool active = allJournals.activeInHierarchy;
        allJournals.SetActive(!active);
    }

    public void ShellFound(Shell.ShellType type)
    {
        int i = (int)type;
        Debug.Log($"shell found of type {type}, index {i}");

        if (amounts[i] == 0)
        {
            checkMarks[i].SetActive(true);
            diary.NewEntry((int) type);
        }

        amounts[i]++;
        amountsGuis[i].SetText($"Amount collected: {amounts[i]}");
    }
}
