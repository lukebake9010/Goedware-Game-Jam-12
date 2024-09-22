using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneMenuUIScript : MonoBehaviour
{
    private enum RuneArcs
    {
        firstArc,
        secondArc,
        thirdArc
    }
    private RuneArcs enabledArcs;
    private bool secondArcEnabled { get { return enabledArcs >= RuneArcs.secondArc; } }
    private bool thirdArcEnabled { get { return enabledArcs >= RuneArcs.thirdArc; } }







    // Start is called before the first frame update
    void Start()
    {
        


    }







}
