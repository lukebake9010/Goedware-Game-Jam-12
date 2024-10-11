using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonBehaviour<SceneLoader>
{

//Since try-catch is used, throws warning over non use of value. Suppress warning.
#pragma warning disable CS0414
    [SerializeField] private bool autoLoadScene = false; //Does Scene automatically load when async done
#pragma warning restore CS0414

    //Display Progress is used for Loading Bars & Public displays of Loading Async Progress
    private float displayProgress = 0;
    public float DisplayProgress { get; private set; }


    new void Awake()
    {
        base.Awake();
    }


    //Function to Start Async Loading Coroutine

    public void StartLoadScene(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex)); //Start Coroutine
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        //Create Empty AsyncOperation
        AsyncOperation loadAsync = null;

        //Attempt to create an Async Operation to Load Scene
        try
        {
            loadAsync = SceneManager.LoadSceneAsync(sceneIndex);
            loadAsync.allowSceneActivation = autoLoadScene;
        }

        //If can't create Async, throw error message and break;
        catch (System.Exception e)
        {
            Debug.LogException(e);
            yield break;
        }

        //If loadAsync wasn't created, break
        if (loadAsync == null) yield break;

        StartCoroutine(OnStartLoadLevelCoroutine());
        while (!breakStartLoadLevel)
        {
            yield return null;
        }

        //While scene isn't loaded
        while (!loadAsync.isDone && displayProgress < 0.9f)
        {
            //Update DisplayProgress
            displayProgress = loadAsync.progress;

            //Synchronous processes to run while Actively Loading Scene
            LoadingScene();

            //Yield Process
            yield return null;
        }

        displayProgress = 1;

        StartCoroutine(OnFinishLoadLevelCoroutine());
        while (!breakFinishLoadLevel)
        {
            yield return null;
        }

        //Enter Logic Here for Booting Scene if necessary (E.g. Key Press, Delay)
        loadAsync.allowSceneActivation = true;

        //End Coroutine
        yield return null;
    }

    //Synchronous function to run while actively Loading New Scene
    public virtual void LoadingScene()
    {

    }

    //When OnStartLoadLevelCoroutine should break
    protected bool breakStartLoadLevel = false;

    //Empty Coroutine to run before Loading Level, "Loadlevel" only continues when broken
    public virtual IEnumerator OnStartLoadLevelCoroutine()
    {
        OnStartLoadLevel();

        breakStartLoadLevel = true;

        yield return null;
    }

    //Synchronous Function to run before Loading Level, "OnStartLoadLevelCoroutine" only continues when complete
    public virtual void OnStartLoadLevel()
    {

    }

    //When OnStartLoadLevelCoroutine should break
    protected bool breakFinishLoadLevel = false;

    //Empty Coroutine to run after Loading Level, "Loadlevel" only finishes when broken
    public virtual IEnumerator OnFinishLoadLevelCoroutine()
    {
        OnFinishLoadLevel();

        breakFinishLoadLevel = true;

        yield return null;
    }

    //Synchronous Function to run after Loading Level, "OnFinishLoadLevelCoroutine" only continues when complete
    public virtual void OnFinishLoadLevel()
    {

    }

}
