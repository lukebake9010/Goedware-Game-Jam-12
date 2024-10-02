using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Summary:
//      Player attributes. Correlate to indexes for the attibute array on the player class
//      and runepage manager.
//      ***FIX DESCRIPTION TO BE ACCURATE TO STRUCTURE***
public enum Attribute
{
    bruh
}

//
// Summary:
//      Enum for mathematical operations for modifiers to the player attributes.
//      Multiplication modifiers are additive.
public enum Operation
{
    addition,
    subtraction,
    multiplier,
    diviser
}

//
// Summary:
//      A modifier to the player attributes.
public struct AttributeModifier
{
    //
    // Summary:
    //      The player attribute to modify.
    public Attribute attribute;
    
    //
    // Summary:
    //      The operation to apply to the modifier.
    public Operation operation;

    //
    // Summary:
    //      The ammount to modify the attribute by.
    public float value;
}
