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
    private int[] amounts = new int[9];

    void Awake()
    {
        checkMarks = GameObject.FindGameObjectsWithTag("Checkmark")
                            .OrderBy(go => go.transform.GetSiblingIndex())
                            .ToArray(); 
        foreach (GameObject checkmark in checkMarks)
        {
            checkmark.SetActive(false);
        }

        var amountsGuisGameObjects = GameObject.FindGameObjectsWithTag("Amount")
                     .OrderBy(go => go.transform.GetSiblingIndex())
                     .ToArray();
        Debug.Log($"checkmars length: {checkMarks.Length}");
        Debug.Log($"amountsGuisGameObjects length: {amountsGuisGameObjects.Length}");
        for (int i = 0; i < 9; i++)
        {
            amountsGuis[i] = amountsGuisGameObjects[i].GetComponent<TextMeshProUGUI>();
        }
        //SwitchTab(Tabs.Shells);
        ToggleJournal();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && allJournals.activeInHierarchy)
        {
            ToggleJournal();
        }
    }

    public void SwitchTab(Tabs tab)
    {
        switch (tab)
        {
            case Tabs.Diary:
                journalDiary.SetActive(true);
                journalShells.SetActive(false);
                journalOptions.SetActive(false);
                break;
            case Tabs.Shells:
                journalDiary.SetActive(false);
                journalShells.SetActive(true);
                journalOptions.SetActive(false);
                break;
            case Tabs.Options:
                journalDiary.SetActive(false);
                journalShells.SetActive(false);
                journalOptions.SetActive(true);
                break;
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

        if (amounts[i] == 0) checkMarks[i].SetActive(true);

        amounts[i]++;
        amountsGuis[i].SetText($"Amount collected: {amounts[i]}");
    }
}
