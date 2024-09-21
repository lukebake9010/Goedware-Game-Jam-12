using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionButtonSubscriber : MonoBehaviour
{
    [SerializeField]
    MenuPage page;
    [SerializeField]
    MenuTransition transition;

    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button == null) return;
        MenuManager menuManager = MenuManager.Instance;
        if (menuManager == null) return;

        if (menuManager is not MenuManagerWithTransitions) return;
        MenuManagerWithTransitions menuManagerWithTransitions = menuManager as MenuManagerWithTransitions;

        if(page == null || transition == null) return;
        button.onClick.AddListener(() => { menuManagerWithTransitions.TransitionToPage(transition, page); });
    }
}
