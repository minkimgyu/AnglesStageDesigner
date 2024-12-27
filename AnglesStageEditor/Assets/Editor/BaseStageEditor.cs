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

            { Name.Tricon, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Chapter1_BossTricorn") },
            { Name.Rhombus, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Chapter2_BossRhombus") },
            { Name.Pentagonic, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Chapter3_BossPentagonic") },
            { Name.Hexahorn, Resources.Load<Sprite>("Images/Enemy/Boss/Angles_Chapter4_BossHexahorn") },

            { Name.YellowTriangle, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyTriYellow") },
            { Name.YellowRectangle, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyRectYellow") },
            { Name.YellowPentagon, Resources.Load<Sprite>("Images/Enemy/Yellow/AnglesEnemyPentaYellow") },
            { Name.YellowHexagon, Resources.Load<Sprite>("Images/Enemy/Yellow/AgnlesEnemyHectaYellow") },

            { Name.RedTriangle, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyTriRed") },
            { Name.RedRectangle, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyRectRed") },
            { Name.RedPentagon, Resources.Load<Sprite>("Images/Enemy/Red/AnglesEnemyPentaRed") },
            { Name.RedHexagon, Resources.Load<Sprite>("Images/Enemy/Red/AgnlesEnemyHectaRed") },

            { Name.OperaTriangle, Resources.Load<Sprite>("Images/Enemy/Opera/AnglesEnemyTriOpera") },
            { Name.OperaRectangle, Resources.Load<Sprite>("Images/Enemy/Opera/AnglesEnemyRectOpera") },
            { Name.OperaPentagon, Resources.Load<Sprite>("Images/Enemy/Opera/AnglesEnemyPentaOpera") },
            { Name.OperaHexagon, Resources.Load<Sprite>("Images/Enemy/Opera/AgnlesEnemyHectaOpera") },

            { Name.Bomb, Resources.Load<Sprite>("Images/AnglesBomb") },
        };

        const float size = 0.6f;

        Dictionary<Name, float> enemyScaleDictionary = new Dictionary<Name, float>()
        {
            { Name.Player_Not_Used, 1f },

            { Name.YellowTriangle, size },
            { Name.YellowRectangle, size },
            { Name.YellowPentagon, size },
            { Name.YellowHexagon, size },

            { Name.RedTriangle, size },
            { Name.RedRectangle, size },
            { Name.RedPentagon, size },
            { Name.RedHexagon, size },

            { Name.OperaTriangle, size },
            { Name.OperaRectangle, size },
            { Name.OperaPentagon, size },
            { Name.OperaHexagon, size },

            { Name.Tricon, size },
            { Name.Rhombus, size },
            { Name.Pentagonic, size },
            { Name.Hexahorn, size },

            { Name.Bomb, size },

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
        EditorGUILayout.LabelField("Export", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        baseDesigner.ExportPackageName = EditorGUILayout.TextField("Package Name", baseDesigner.ExportPackageName);
        baseDesigner.FileLocation = EditorGUILayout.TextField("Location To Export", baseDesigner.ExportPackageLocation);

        if (GUILayout.Button("Build Package")) baseDesigner.ExportData();


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
