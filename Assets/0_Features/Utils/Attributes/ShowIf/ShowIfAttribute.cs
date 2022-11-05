using System;
using System.Diagnostics;
using UnityEngine;

namespace _0_Features.Utils.Attributes.ShowIf
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionPropertyName { get; private set; } = null;
        public bool Condititon { get; private set; } = true;

        public ShowIfAttribute(string conditionPropertyName)
        {
            ConditionPropertyName = conditionPropertyName;
        }

        public ShowIfAttribute(string conditionPropertyName, bool condititon) : this(conditionPropertyName)
        {
            Condititon = condititon;
        }
    }
}
