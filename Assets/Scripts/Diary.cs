using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI page1;
    [SerializeField] TextMeshProUGUI page2;
    [SerializeField] List<string> allEntries = new List<string>();

    [SerializeField] Button nextPageButton;
    [SerializeField] Button previousPageButton;

    List<string> paginatedStrings = new List<string>();
    List<string> entriesToShow = new List<string>();
    int currentPageIndex = 0;

    public void NewEntry(int entryIndex)
    {
        entriesToShow.Add(allEntries[entryIndex]);
        DoPagination();
    }

    void Awake()
    {
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);
    }

    void Start()
    {
        NewEntry(10);
    }

    void DoPagination()
    {
        paginatedStrings.Clear();

        StringBuilder sb = new StringBuilder();
        foreach (string entry in entriesToShow)
        {
            sb.Append(entry);
            sb.AppendLine();
            sb.AppendLine();
        }
        string rest = sb.ToString();

        while (rest.Length > 0)
        {
            int nextPageLength = 400;
            nextPageLength = Math.Min(nextPageLength, rest.Length);
            string pageString = rest.Substring(0, nextPageLength);
            paginatedStrings.Add(pageString);
            rest = rest.Substring(pageString.Length);
        }
        UpdateVisiblePages(currentPageIndex);
    }

    public void NextPage()
    {
        Debug.Log("nextpage");
        if (paginatedStrings.Count > currentPageIndex + 2)
        {
            currentPageIndex += 2;
            UpdateVisiblePages(currentPageIndex);
        }
    }

    public void PreviousPage()
    {
        Debug.Log("previouspage");
        currentPageIndex -= 2;
        currentPageIndex = Math.Max(currentPageIndex, 0);
        UpdateVisiblePages(currentPageIndex);
    }

    void UpdateVisiblePages(int leftPageIndex)
    {

        if (paginatedStrings.Count + 1 >= leftPageIndex)
        {
            page1.text = paginatedStrings[leftPageIndex];
        }
        if (paginatedStrings.Count > leftPageIndex + 1)
        {
            page2.text = paginatedStrings[leftPageIndex + 1];
        }
        else
        {
            page2.text = "";
        }
    }

}
