using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBeacon : Singleton<SpellBeacon>
{
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
