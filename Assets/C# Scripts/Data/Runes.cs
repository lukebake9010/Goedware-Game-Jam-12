using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Summary:
//      Enum of all existing capstones. To add a new capstone, add it to the enum, and append all switch statements with a case.
public enum Capstone 
{
    Capstone1,
    Capstone2,
    Capstone3,
    Capstone4,
    Capstone5,
    Capstone6,
    Capstone7,
    Capstone8,
    Capstone9
}

//
// Summary:
//      Enum of all existing shards. To add a new shard, add it to the enum, and append the extension dictionary lookup.
public enum Shard
{
    Shard1,
    Shard2,
    Shard3,
    Shard4,
    Shard5
}

//
// Summary:
//      Enum of all existing aura runes. To add a new aura rune, add it to the enum, and append the extension dictionary lookup.
public enum Aurarune
{
    Aurarune1,
    Aurarune2,
    Aurarune3,
    Aurarune4,
    Aurarune5
}

public static class RuneExtensions
{
    private static readonly Dictionary<Shard, ShardObj> shardObjs = new Dictionary<Shard, ShardObj>()
    {
        { Shard.Shard1, new ShardObj()
            {
                effect = new AttributeModifier[]
                {
                    new AttributeModifier()
                    {

                    }
                },
                corruptedEffect = new AttributeModifier[]
                {
                    new AttributeModifier()
                    {

                    }
                }
            }
        }
    };

    private static readonly Dictionary<Aurarune, AuraruneObj> auraRuneObjs = new Dictionary<Aurarune, AuraruneObj>()
    {
        {
            Aurarune.Aurarune1, new AuraruneObj() 
        }
    };

    public static ShardObj? getShard(Shard rune)
    {
        if(shardObjs.TryGetValue(rune, out ShardObj shard))
            return shard;
        return null;
    }

    public static AuraruneObj? getAuraRune(Aurarune rune)
    {
        if (auraRunes.TryGetValue(rune, out AuraruneObj aurarune))
            return aurarune;
        return null;
    }
}

//
// Summary:
//      Data class for shards placed on a runepage grid.
public struct ShardObj
{
    //
    // Summary:
    //      The effect on the player attributes that "slotting" this rune provides. 
    public AttributeModifier[] effect;
    
    //
    // Summary:
    //      The effect on the player attributes that this rune provides when it is corrupted.
    public AttributeModifier[] corruptedEffect;

    //
    // Summary:
    //      Debug function to display the rune's data.
    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }
}

//
// Summary:
//      Data class for aura runes placed on a runepage grid.
public struct AuraruneObj
{
    //
    // Summary:
    //      The hex range of the effect of this aurarune.
    public int range;

    //
    // Summary:
    //      Whether this rune should stack with other aurarunes.
    public bool stackable;

    //
    // Summary:
    //      The priority of this rune's effect. Effects apply highest priority => lowest priority, and end with the first "non stackable" rune.
    //      This means non-stackable runes only do not stack with lower priority runes, but WILL stack with higher priority runes that are stackable.
    public int priority;

    #region MOVE THIS
    //
    // Summary:
    //      The rotation of the rune, as a factor of Clockwise 1/6th rotations.
    public int Rotation;

    //
    // Summary:
    //      The nodes that comprise this multislot structure.
    //      The "Core" cell is at 0,0,0.
    public Axial[] nodes;

    //
    // Summary:
    //      Optimises rotation as a number between -2 & 3.
    private void optimiseRotation()
    {
        Rotation %= 6;
        if (Rotation > 3)
            Rotation -= 6;
    }

    //
    // Summary:
    //      Rotates the nodes clockwise around the Core
    public void rotateClockwise()
    {
        foreach(Axial node in nodes)
            node.Rotate(true);
        Rotation++;
        optimiseRotation();
    }

    //
    // Summary:
    //      Rotates the nodes counterclockwise around the Core
    public void rotateAntiClockwise()
    {
        foreach (Axial node in nodes)
            node.Rotate(false);
        Rotation++;
        optimiseRotation();
    }
    #endregion

    //
    // Summary:
    //      Debug function to display the rune's data.
    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }
}