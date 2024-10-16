using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T:SingletonBehaviour<T>, new()
{

    // Refers to the instance.
    public static T Instance { get; private set; }
    
    // Will be created in runtime if not existing already, for creation programmatically
    public static T GetInstance()
    {
        if (Instance == null)
            Instance = new T();
        return Instance;
    }

    // Can be used attatched to an object in the scene, aslong as there's only one.
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning(this.GetType() + " already exists in this scene, this class will not be created again. Are there multiple objects with this script or an undestroyed cross-scene instance present?");

#pragma warning disable CS0162
#if UNITY_EDITOR
            //If in editor, destroy will not function. Rare Edge case requires a destroy immediate call.
            //preprocessing condition will remove this from build

            if (!Application.isPlaying)
                DestroyImmediate(this);
            return;
#endif

            Destroy(this);
            return;
#pragma warning restore CS0162
        }

        Instance = (T)this;
    }
}

public class Singleton<T> where T : SingletonBehaviour<T>, new()
{
    // Refers to the instance.
    public static T Instance { get; private set; }

    // Will be created in runtime if not existing already, for creation programmatically
    public static T GetInstance()
    {
        if (Instance == null)
            Instance = new T();
        return Instance;
    }
}