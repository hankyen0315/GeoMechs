using System;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class ConditionalDisableAttribute : PropertyAttribute
{
    public string ConditionalSourceField;
    public bool DisableValue;

    public ConditionalDisableAttribute(string conditionalSourceField, bool disableValue)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.DisableValue = disableValue;
    }
}
