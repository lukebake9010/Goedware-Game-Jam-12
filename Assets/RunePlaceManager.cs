using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlaceManager : Singleton<RunePlaceManager>
{
 
    new void Awake() { base.Awake(); }

    private GameObject calledSlot;

    public void StartPlace(Rune rune)
    {
        StartCoroutine(PlaceRune(rune));
    }

    private IEnumerator PlaceRune(Rune rune)
    {
        calledSlot = null;
        while (calledSlot == false)
        {
            yield return null;
        }

    }

    public void CallSlot(GameObject slot)
    {
        calledSlot = slot;
    }
}
