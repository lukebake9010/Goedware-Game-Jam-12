using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunePlaceSubscriber : MonoBehaviour
{
    [SerializeField]
    private Button button;


    private void Awake()
    {
        if(button == null) return;
        RunePlaceManager runePlaceManager = RunePlaceManager.Instance;
        if(runePlaceManager == null ) return;
        button.onClick.AddListener(() => { runePlaceManager.CallSlot(gameObject); });

    }

}
