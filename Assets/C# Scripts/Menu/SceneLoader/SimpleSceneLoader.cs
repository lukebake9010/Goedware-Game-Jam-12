using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    [SerializeField]
    private bool AutoLoadScene = false; //Does Scene automatically load when async done


    //Display Progress is used for Loading Bars & Public displays of Loading Async Progress
    private float displayProgress = 0;
    public float DisplayProgress
    {
        get { return displayProgress; }
        private set { displayProgress = value; }
    }


    //Function to Start Async Loading Coroutine
    public void StartLoadScene(string sceneName)
    {
        StartCoroutine(Loadlevel(sceneName)); //Start Coroutine
    }

    IEnumerator Loadlevel(string sceneName)
    {
        //Create Empty AsyncOperation
        AsyncOperation loadAsync = null;

        //Attempt to create an Async Operation to Load Scene
        try
        {
            loadAsync = SceneManager.LoadSceneAsync(sceneName);
        }
        //If can't create Async, throw error message and break;
        catch (System.Exception e)
        {
            Debug.LogException(e);
            yield break;
        }

        //If loadAsync wasn't created, break
        if (loadAsync == null) yield break;


        //While scene isn't loaded
        while (!loadAsync.isDone && displayProgress < 0.9f)
        {
            //Update DisplayProgress
            displayProgress = loadAsync.progress;

            //Output Progress in console
            Debug.Log("Loading Scene: " + sceneName + " , Progress: " + displayProgress);

            //Yield Process
            yield return null;
        }

        //End Coroutine
        yield return null;
    }
}
