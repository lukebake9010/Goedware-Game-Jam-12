using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that shows how you can make a subclass out of SceneLoader to Load a Scene with Custom Functionality
public class SceneLoaderChildTest : SceneLoader
{

    //How to override the start & end coroutines
    public override IEnumerator OnStartLoadLevelCoroutine()
    {
        OnFinishLoadLevel(); //Necessary at start

        yield return new WaitForSeconds(3f); //Put whatever functionality you want here

        breakStartLoadLevel = true; //Necessary at end
    }

    //How to override Active Loading Functionality
    public override void LoadingScene()
    {
        Debug.Log("Mogging");
    }

    //How to override start & end synchronous processes
    public override void OnFinishLoadLevel()
    {
        Debug.Log("Child Debug Log");
    }

}
