using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;

abstract public class BaseStageDesigner : MonoBehaviour
{
    protected PreviewSpawner previewSpawner;
    protected Dictionary<Name, Sprite> enemyImageDictionary;
    protected Dictionary<Name, float> enemyScaleDictionary;

    [HideInInspector] public TextAsset fileToLoad;

    protected FileIO fileIO = new FileIO(new JsonParser(), ".txt");

    protected string fileName = "StageData";
    public string FileName { get { return fileName; } set { fileName = value; } }

    protected string fileLocation = "Editor/Save";
    public string FileLocation { get { return fileLocation; } set { fileLocation = value; } }

    protected string exportPackageName = "LevelPackage";
    public string ExportPackageName { get { return exportPackageName; } set { exportPackageName = value; } }

    protected string exportPackageLocation = "Editor/Save";
    public string ExportPackageLocation { get { return exportPackageLocation; } set { exportPackageLocation = value; } }

    [MenuItem("StageEditor/Build Release[For Developer Only!! Not Designer!!]", false, 1)]
    public static void BuildRelease()
    {
        List<string> paths = new List<string>();

        paths.Add("Assets/Editor");
        paths.Add("Assets/Resources");
        paths.Add("Assets/Scripts");

        AssetDatabase.ExportPackage(paths.ToArray(), "AnglesStageDesigner_n_n_n.unitypackage",
                    ExportPackageOptions.Recurse |
                    ExportPackageOptions.IncludeDependencies |
                    ExportPackageOptions.Interactive);
    }

    public void ExportData()
    {
        AssetDatabase.ExportPackage($"Assets/{exportPackageLocation}", $"{exportPackageName}.unitypackage",
                    ExportPackageOptions.Recurse | 
                    ExportPackageOptions.IncludeDependencies | 
                    ExportPackageOptions.Interactive);
    }

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

            // 다른 씬을 갔다오면 초기화가 되어있지 않은 경우가 있음
            // 이를 추가로 해결해줘야한다.
            if (previewSpawner.NowInitialized == false)
            {
                DestroyImmediate(previewSpawner.gameObject);
                CreatePreviewSpawner(previewerPrefab);
                _OnValidate(); // 기존 데이터를 바탕으로 초기화 해준다.
            }
        }
        else
        {
            CreatePreviewSpawner(previewerPrefab);
        }
    }

    async void OnChanged()
    {
        await Task.Delay(100);
        _OnValidate();
    }

    void OnValidate() 
    { 
        OnChanged(); 
    }

    protected abstract void _OnValidate();

    void CreatePreviewSpawner(Previewer previewerPrefab)
    {

        ///
        ///
        //
        //
        //
        //

        //
        //
        //

        //
        //

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
        if (previewSpawner == null) return;

        Previewer previewer = previewSpawner.Create(enemyImageDictionary[spawnData.name], enemyScaleDictionary[spawnData.name]);
        previewer.transform.position = new Vector2(spawnData.spawnPosition.x, spawnData.spawnPosition.y);
    }

    public void RemovePreviewer()
    {
        if (previewSpawner == null) return;

        previewSpawner.Clear();
    }
}
