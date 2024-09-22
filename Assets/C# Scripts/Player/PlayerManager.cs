using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    PlayerController controller;

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
