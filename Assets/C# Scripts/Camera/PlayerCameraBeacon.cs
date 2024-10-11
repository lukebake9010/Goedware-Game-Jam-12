using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraBeacon : SingletonBehaviour<PlayerCameraBeacon>
{
    
    new void Awake()
    {
        base.Awake();
    }

    public Camera GetCamera()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        if(cam == null ) return new Camera();
        return cam;
    }

}
