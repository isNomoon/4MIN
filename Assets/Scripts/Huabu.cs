using UnityEngine;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    [System.Serializable]
    public class PageEntry
    {
        public string pageName;      // 画布名称（如 "主菜单"）
        public GameObject pageObject; // 对应的画布
    }

    public PageEntry[] pages = new PageEntry[5]; // 5张画布
    private string currentPageName = "";

    void Start()
    {
        // 默认显示第一张
        if (pages.Length > 0)
            SwitchToPage(pages[0].pageName);
    }

    // ===== 切换到指定画布（通过名字） =====
    public void SwitchToPage(string pageName)
    {
        foreach (PageEntry entry in pages)
        {
            entry.pageObject.SetActive(entry.pageName == pageName);
        }
        currentPageName = pageName;
    }

    // ===== 下一页 =====
    public void NextPage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i].pageName == currentPageName)
            {
                int next = (i + 1) % pages.Length; // 循环到第一张
                SwitchToPage(pages[next].pageName);
                return;
            }
        }
    }

    // ===== 上一页 =====
    public void PreviousPage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i].pageName == currentPageName)
            {
                int prev = (i - 1 + pages.Length) % pages.Length; // 循环到最后一张
                SwitchToPage(pages[prev].pageName);
                return;
            }
        }
    }
}