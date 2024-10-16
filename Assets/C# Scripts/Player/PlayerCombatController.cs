using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    // Enums are non-nullable. Default value is the default value of the constant equivalent.
    // By default, this value is int.
    // Can be manually specified by (or similar):
    // private enum Spell : ushort
    private enum Spell 
    {
        InvalidSpell,
        Earth,
        Wind,
        Fire,
    }

    [Serializable]
    private struct SpellPrefab
    {
        public Spell spell;
        public GameObject prefab;
    }

    [SerializeField]
    private SpellPrefab[] spellPrefabs;

    private SpellPrefab currentSpell;

    private void Awake()
    {
        if (spellPrefabs == null) return;
        currentSpell = spellPrefabs[0];
    }

    public void Attack()
    {
        if (spellPrefabs == null) return;
        PlayerManager playerManager = PlayerManager.Instance;
        if(playerManager == null) return;
        Vector3 spellStart = playerManager.GetSpellStartPosition();
        Quaternion spellRotation = playerManager.GetRotation();
        if (currentSpell.spell == Spell.InvalidSpell || currentSpell.prefab == null) return;
        SpawnSpell(currentSpell, spellStart, spellRotation);        
    }

    private void SpawnSpell(SpellPrefab spellPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject spellObject = spellPrefab.prefab;
        Instantiate(spellObject, position, rotation);
    }
}
