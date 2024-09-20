using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    [SerializeField]
    private Animation transitionAnimation;

    public bool finishedTransition = false;
    public bool Finished
    {
        get { return finishedTransition; }
        private set { finishedTransition = value; }
    }


    public void Transition()
    {
        ResetTransition();
        if (transitionAnimation == null) return;
        transitionAnimation.Play();
    }

    private void ResetTransition()
    {
        finishedTransition = false;
    }

    public void FinishedTransition()
    {
        finishedTransition |= true;
    }

}
