using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

abstract public class BaseStageDesigner : MonoBehaviour
{
    protected PreviewSpawner previewSpawner;
    protected Dictionary<Name, Sprite> enemyImageDictionary;
    protected Dictionary<Name, float> enemyScaleDictionary;

    [HideInInspector] public TextAsset fileToLoad;

    protected FileIO fileIO = new FileIO(new JsonParser(), ".txt");

    protected string fileName = "StageData";
    public string FileName { get { return fileName; } set { fileName = value; } }

    protected string fileLocation = "/Editor/Save/";
    public string FileLocation { get { return fileLocation; } set { fileLocation = value; } }

    public void Initialize(
        Dictionary<Name, Sprite> enemyImageDictionary,
        Dictionary<Name, float> enemyScaleDictionary,
        Previewer previewerPrefab)
    {
        this.enemyImageDictionary = enemyImageDictionary;
        this.enemyScaleDictionary = enemyScaleDictionary;

        Transform childTransform = transform.Find("PreviewerSpawner");
        if (childTransform != null)// 존재한다면 아래 진행하지 않음
        {
            previewSpawner = childTransform.GetComponent<PreviewSpawner>();
            return;
        }

        CreatePreviewSpawner(previewerPrefab);
    }

    void OnValidate() { EditorApplication.delayCall += _OnValidate; }

    protected abstract void _OnValidate();

    void CreatePreviewSpawner(Previewer previewerPrefab)
    {
        GameObject activatedPreviewerParent = new GameObject("activatedPreviewerParent");
        GameObject deactivatedPreviewerParent = new GameObject("deactivatedPreviewerParent");

        GameObject previewerSpawner = new GameObject("PreviewerSpawner");
        previewerSpawner.transform.SetParent(transform);

        /////
        // 부모 변경
        activatedPreviewerParent.transform.SetParent(previewerSpawner.transform);
        deactivatedPreviewerParent.transform.SetParent(previewerSpawner.transform);

        previewSpawner = previewerSpawner.AddComponent<PreviewSpawner>();
        previewSpawner.Initialize(previewerPrefab, 10, activatedPreviewerParent.transform, deactivatedPreviewerParent.transform);
    }

    public abstract void SaveData();
    public abstract void LoadData();

    protected void CreatePreviewer(SpawnData spawnData)
    {
        Previewer previewer = previewSpawner.Create(enemyImageDictionary[spawnData.name], enemyScaleDictionary[spawnData.name]);
        previewer.transform.position = new Vector2(spawnData.spawnPosition.x, spawnData.spawnPosition.y);
    }

    public void RemovePreviewer()
    {
        previewSpawner.Clear();
    }
}
