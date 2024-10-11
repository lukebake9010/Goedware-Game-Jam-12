using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : SingletonBehaviour<MenuManager>
{
    [SerializeField]
    MenuPage currentPage;

    new void Awake()
    {
        base.Awake();

        if (currentPage == null) return;

        currentPage.TogglePage(true);
    }

    public void MoveToPage(MenuPage page)
    {
        if(currentPage == null || page == null) return;
        currentPage.TogglePage(false);
        page.TogglePage(true);
    }

    public void LoadPage(MenuPage page)
    {
        if (currentPage != null)
        {
            MoveToPage(page);
            return;
        }
        currentPage = page;
        currentPage.TogglePage(true);
    }
}
