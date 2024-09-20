using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    [SerializeField]
    private GameObject pageObject;

    public void TogglePage(bool toggled)
    {
        if (pageObject == null) return;
        pageObject.SetActive(toggled);
    }
}
