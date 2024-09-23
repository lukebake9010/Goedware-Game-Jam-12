using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    PlayerController controller;
    [SerializeField]
    PlayerCombatController combatController;

    SpellBeacon spellBeacon;

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public Quaternion GetRotation()
    {
        return gameObject.transform.rotation;
    }


    private SpellBeacon FindSpellStart()
    {
        SpellBeacon spellBeacon = SpellBeacon.Instance;
        if (spellBeacon == null) return null;
        return spellBeacon;
    }

    public Vector3 GetSpellStartPosition()
    {
        if(spellBeacon == null) spellBeacon = FindSpellStart();
        if (spellBeacon == null) return Vector3.zero;
        return spellBeacon.GetPosition();
    }

    public void PlayerCombatAttack()
    {
        if (combatController == null) return;
        combatController.Attack();
    }
}
