using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLoadButtonSubscriber : MonoBehaviour
{
    //int is non-nullable
    //default value is 0, which IS a valid index.
    //solution: decrement default value to invalid index and use <0 to check if valid.
    [SerializeField]
    private int sceneIndex = -1;

    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button == null) return;

        if (sceneIndex < 0) return;
        button.onClick.AddListener(() => { SceneLoader.Instance.StartLoadScene(sceneIndex); });
    }
}
