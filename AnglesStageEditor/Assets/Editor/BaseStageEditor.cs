using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseStageEditor : Editor
{
    protected GUIStyle labelStyle;
    protected SerializedProperty fileToLoad;

    protected virtual void OnEnable()
    {
        labelStyle = new GUIStyle();
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.normal.textColor = Color.white;
        labelStyle.fontSize = 20;
        labelStyle.fontStyle = UnityEngine.FontStyle.Bold;

        fileToLoad = serializedObject.FindProperty("fileToLoad");

        Dictionary<Name, Sprite> enemyImageDictionary = new Dictionary<Name, Sprite>()
        {
            { Name.Player_Not_Used, Resources.Load<Sprite>("Images/Player_Not_Used") },

            { Name.Tricon, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Boss_Tricorn") },
            { Name.Rhombus, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Boss_Rhombus") },
            { Name.Pentagonic, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Boss_Pentagonic") },

            { Name.YellowTriangle, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyTriYellow") },
            { Name.YellowRectangle, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyRectYellow") },
            { Name.YellowPentagon, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyPentaYellow") },
            { Name.YellowHexagon, Resources.Load<Sprite>("Images/Enemy/Yellow/AgnlesEnemyHectaYellow") },

            { Name.RedTriangle, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyTriRed") },
            { Name.RedRectangle, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyRectRed") },
            { Name.RedPentagon, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyPentaRed") },
            { Name.RedHexagon, Resources.Load<Sprite>("Images/Enemy/Red/AgnlesEnemyHectaRed") },

            { Name.Bomb, Resources.Load<Sprite>("Images/AnglesBomb") },
        };

        Dictionary<Name, float> enemyScaleDictionary = new Dictionary<Name, float>()
        {
            { Name.Player_Not_Used, 1f },

            { Name.YellowTriangle, 0.4f },
            { Name.YellowRectangle, 0.4f },
            { Name.YellowPentagon, 0.5f },
            { Name.YellowHexagon, 0.5f },

            { Name.RedTriangle, 0.4f },
            { Name.RedRectangle, 0.4f },
            { Name.RedPentagon, 0.45f },
            { Name.RedHexagon, 0.45f },

            { Name.Tricon, 0.6f },
            { Name.Rhombus, 0.6f },
            { Name.Pentagonic, 0.75f },

            { Name.Bomb, 0.7f },

        };

        Previewer previewerPrefab = Resources.Load<Previewer>("Prefabs/Previewer");

        BaseStageDesigner stageDesigner = (BaseStageDesigner)target;
        stageDesigner.transform.position = Vector3.zero;
        stageDesigner.Initialize(enemyImageDictionary, enemyScaleDictionary, previewerPrefab);
    }

    protected void DrawBasicInspector(BaseStageDesigner baseDesigner)
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Setting", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        baseDesigner.FileName = EditorGUILayout.TextField("File Name", baseDesigner.FileName);
        baseDesigner.FileLocation = EditorGUILayout.TextField("Location", baseDesigner.FileLocation);

        if (GUILayout.Button("Save")) baseDesigner.SaveData();
        EditorGUILayout.Space();


        EditorGUILayout.PropertyField(fileToLoad, new GUIContent("File To Load"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Load")) baseDesigner.LoadData();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stage", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
    }
}
