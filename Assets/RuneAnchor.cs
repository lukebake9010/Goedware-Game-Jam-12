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
        Rune isRune = rune.GetComponent<Rune>();
        return (isRune != null);
    }

    private bool IsRune()
    {
        Rune isRune = runeObject.GetComponent<Rune>();
        return (isRune != null);
    }

    private GameObject GetRuneObject()
    {
        return runeObject;
    }

    private Rune GetRune()
    {
        Rune rune = runeObject.GetComponent<Rune>();
        return rune;
    }
}
