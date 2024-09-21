using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    [SerializeField]
    private Animator transitionAnimation;

    [SerializeField]
    private float transitionTime = 6f;

    public bool startedTransition = false;
    public bool loadedTransition = false;
    public bool Loaded{ get; private set;}

    public bool finishedTransition = false;
    public bool Finished { get; private set;}


    public void Transition()
    {
        ResetTransition();
        if (transitionAnimation == null) return;
        StartedTransition();
    }

    private void ResetTransition()
    {
        startedTransition = false;
        loadedTransition = false;
        finishedTransition = false;
    }

    public virtual void StartedTransition()
    {
        startedTransition |= true;
        transitionAnimation.SetBool("Started", true);
        TimeOutTransition();
    }

    //Should be called by the entry animation of a transition
    public virtual void LoadedTransition()
    {
        loadedTransition |= true;
        transitionAnimation.SetBool("Loaded", true);
    }

    //Should be called by the exit animation of a transition
    public virtual void FinishedTransition()
    {
        finishedTransition |= true;
        StopCoroutine(TimeOutTransition());
        transitionAnimation.SetBool("Finished", true);
    }


    private IEnumerator TimeOutTransition()
    {
        yield return new WaitForSeconds(transitionTime);

        if(startedTransition && (!loadedTransition || !finishedTransition))
        {
            ResetTransition();
        }
    }
}
