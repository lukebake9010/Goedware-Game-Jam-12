using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class ShardScrollRectValidator : Singleton<ShardScrollRectValidator>
{
    public static void ValidateScrollRect()
    {
        GameObject obj = GetInstance().gameObject;
        if (obj.GetComponentInChildren<RectTransform>().rect.height > obj.transform.GetComponent<RectTransform>().rect.height)
            obj.GetComponent<ScrollRect>().enabled = true;
        else
            obj.GetComponent<ScrollRect>().enabled = false;
    }
}
