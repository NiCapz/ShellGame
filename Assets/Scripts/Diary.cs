using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI page1;
    [SerializeField] TextMeshProUGUI page2;
    [SerializeField] List<string> allEntries = new List<string>();

    List<string> paginatedStrings = new List<string>();
    List<string> entriesToShow = new List<string>();
    int currentPageIndex = 0;

    public void NewEntry(int entryIndex)
    {
        entriesToShow.Add(allEntries[entryIndex]);
        DoPagination();
    }

    void Start()
    {
        NewEntry(0);
    }

    void DoPagination()
    {
        paginatedStrings.Clear();
        
        // regenerate text to paginate
        StringBuilder sb = new StringBuilder();
        foreach (string entry in entriesToShow)
        {
            sb.Append(entry);
            sb.AppendLine();
        }
        string rest = sb.ToString();
        Debug.Log(rest);
        
        while (rest.Length > 0)
        {
            int nextPageLength = 470;
            nextPageLength = Math.Min(nextPageLength, rest.Length);
            
            Debug.Log($"rest length {rest.Length}");
            Debug.Log($"nextpagelengtht {nextPageLength}");
            
            string pageString = rest.Substring(0, nextPageLength);
            paginatedStrings.Add(pageString);
            rest = rest.Substring(pageString.Length);
        }
        UpdateVisiblePages(currentPageIndex);
    }

    public void TurnPage()
    {
        currentPageIndex += 2;
        UpdateVisiblePages(currentPageIndex);
    }

    void UpdateVisiblePages(int leftPageIndex)
    {
        if (paginatedStrings.Count > leftPageIndex) page1.text = paginatedStrings[leftPageIndex];
        if (paginatedStrings.Count > leftPageIndex + 1) page2.text = paginatedStrings[leftPageIndex + 1];
    }
}
