using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RuneAnchor : MonoBehaviour
{
    [SerializeField]
    GameObject runeObject;


    private bool IsRune(GameObject rune)
    {
        MonoBehaviourRune isRune = rune.GetComponent<MonoBehaviourRune>();
        return (isRune != null);
    }

    private bool IsRune()
    {
        MonoBehaviourRune isRune = runeObject.GetComponent<MonoBehaviourRune>();
        return (isRune != null);
    }

    private GameObject GetRuneObject()
    {
        return runeObject;
    }

    private MonoBehaviourRune GetRune()
    {
        MonoBehaviourRune rune = runeObject.GetComponent<MonoBehaviourRune>();
        return rune;
    }
}
