using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerWithTransitions : MenuManager
{

    public void TransitionToPage(MenuTransition transition, MenuPage page)
    {
        if (transition == null)
        {
            if (page == null) return;
            else MoveToPage(page);
        }
        StartCoroutine(WaitForTransitionThenMove(transition, page));
    }

    private IEnumerator WaitForTransitionThenMove(MenuTransition transition, MenuPage page)
    {
        transition.Transition();
        while (!transition.Finished)
        {
            yield return null;
        }
        MoveToPage(page);
    }
}
