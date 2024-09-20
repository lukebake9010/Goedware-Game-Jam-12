using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLoadButtonSubscriber : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;

    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button == null) return;

        if (sceneIndex == null) return;
        button.onClick.AddListener(() => { SceneLoader.GetInstance().StartLoadScene(sceneIndex); });
    }
}
