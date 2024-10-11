using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlaceManager : SingletonBehaviour<RunePlaceManager>
{
 
    new void Awake() { base.Awake(); }

    private GameObject calledSlot;

    public void StartPlace(MonoBehaviourRune rune)
    {
        StartCoroutine(PlaceRune(rune));
    }

    private IEnumerator PlaceRune(MonoBehaviourRune rune)
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
