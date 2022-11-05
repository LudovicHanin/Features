using UnityEditor;
using UnityEngine;

namespace _0_Features.Utils.Attributes.ShowIf.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeEditor : PropertyDrawer
    {
        private ShowIfAttribute _showIfAttribute;

        private SerializedProperty _conditionField;

        private float _propertyHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _showIfAttribute = attribute as ShowIfAttribute;

            if (_showIfAttribute == null) return;

            _conditionField = property.serializedObject.FindProperty(_showIfAttribute.ConditionPropertyName);

            if (_conditionField == null)
            {
                Debug.LogError($"Name of the condition not found {_showIfAttribute.ConditionPropertyName} in {_showIfAttribute.GetType()}");
                return;
            }
        
            bool value = _conditionField.boolValue;

            _propertyHeight = base.GetPropertyHeight(property, label);

            if (_showIfAttribute.Condititon == value)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                _propertyHeight = 0f;
            }
        }
    }
}