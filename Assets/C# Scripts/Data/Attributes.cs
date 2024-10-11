using System;

//
// Summary:
//      Player attributes. Correlate to indexes for the attibute array on the player class
//      and runepage manager.
//      ***FIX DESCRIPTION TO BE ACCURATE TO STRUCTURE***
public enum Attribute
{
    Bruh_Example
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

    //
    // Summary:
    //      Display function for the modifier
    public override string ToString() => AttributeExtensions.AttributeDisplayName(attribute) + " " + AttributeExtensions.OperationDisplayName(operation) + " " + value.ToString();
}

//
// Summary:
//      Class for extension functions based on enums.
public static class AttributeExtensions
{
    //
    // Summary:
    //      Returns the display name for the attribute.
    public static string AttributeDisplayName(Attribute attribute) => nameof(attribute).Replace('_', ' ');

    //
    // Summary:
    //      Returns the display description for the attribute.
    public static string AttributeDescription(Attribute attribute)
    {
        return attribute switch
        {
            Attribute.Bruh_Example => "This is the description of Bruh Example.",
            _ => throw new ArgumentException()
        };
    }

    //
    // Summary:
    //      Returns the display string for the mathematical operations.
    public static string OperationDisplayName(Operation operation)
    {
        return operation switch
        {
            Operation.addition => "+",
            Operation.subtraction => "-",
            Operation.multiplier => "x",
            Operation.diviser => "/",
            _ => throw new ArgumentException()
        };
    }
}
