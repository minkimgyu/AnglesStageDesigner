using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

/// <summary>
/// A custom property drawer for vectors type structures.
/// </summary>
/// <seealso cref="UnityEditor.PropertyDrawer" />
[CustomPropertyDrawer(typeof(SerializableVector2))]
public class VectorPropertyDrawer : PropertyDrawer
{
    /// <summary>
    /// A dictionary lookup of field counts keyed by class type name.
    /// </summary>
    private static Dictionary<string, int> _fieldCounts = new Dictionary<string, int>();

    /// <summary>
    /// Called when the GUI is drawn.
    /// </summary>
    /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
    /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
    /// <param name="label">The label of this property.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var fieldCount = GetFieldCount(property);

        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        EditorGUIUtility.labelWidth = 14f;
        float fieldWidth = contentPosition.width / fieldCount;
        bool hideLabels = contentPosition.width < 85;
        contentPosition.width /= fieldCount;

        using (var indent = new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
        {
            for (int i = 0; i < fieldCount; i++)
            {
                if (!property.NextVisible(true))
                {
                    break;
                }

                label = EditorGUI.BeginProperty(contentPosition, new GUIContent(property.displayName), property);
                EditorGUI.PropertyField(contentPosition, property, hideLabels ? GUIContent.none : label);
                EditorGUI.EndProperty();

                contentPosition.x += fieldWidth;
            }
        }
    }

    /// <summary>
    /// Gets the field count for the specified property.
    /// </summary>
    /// <param name="property">The property for which to get the field count.</param>
    /// <returns>The field count of the property.</returns>
    private int GetFieldCount(SerializedProperty property)
    {
        int count;
        if (!_fieldCounts.TryGetValue(property.type, out count))
        {
            var children = property.Copy().GetEnumerator();
            while (children.MoveNext())
            {
                count++;
            }

            _fieldCounts[property.type] = count;
        }

        return count;
    }
}