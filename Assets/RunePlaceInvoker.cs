using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlaceInvoker : MonoBehaviour
{
    [SerializeField]
    Rune rune;

    public void InvokeRunePlace()
    {
        RunePlaceManager runePlaceManager = RunePlaceManager.Instance;
        if (runePlaceManager == null || rune == null) return;
        runePlaceManager.StartPlace(rune);
    }
}
