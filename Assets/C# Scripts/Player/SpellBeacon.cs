using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBeacon : SingletonBehaviour<SpellBeacon>
{
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
