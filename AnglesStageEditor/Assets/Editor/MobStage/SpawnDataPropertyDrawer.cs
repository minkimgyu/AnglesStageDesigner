using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

abstract public class BaseDrawer
{
    protected SerializedProperty _property;

    public BaseDrawer(SerializedProperty property)
    {
        _property = property;
    }

    public virtual void Draw() { }
    public virtual void Draw(Difficulty difficulty) { }
}


public class SpawnDataPropertyDrawer : BaseDrawer
{
    public SpawnDataPropertyDrawer(SerializedProperty spawnDataProperty) : base(spawnDataProperty)
    {
    }

    public override void Draw(Difficulty difficulty)
    {
        // Ofcourse you also want to change the list size here
        _property.arraySize = EditorGUILayout.IntField("Size", _property.arraySize);

        for (int i = 0; i < _property.arraySize; i++)
        {
            var dialogue = _property.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(dialogue, new GUIContent("Dialogue " + i), true);
        }
    }
}
