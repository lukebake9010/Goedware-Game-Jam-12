using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T>, new()
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
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning(this.GetType() + " already exists in this scene, this class will not be created again. Are there multiple objects with this script or an undestroyed cross-scene instance present?");

            Destroy(this);
            return;
        }

        Instance = (T)this;
    }
}
