using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

abstract public class BaseStageDesigner : MonoBehaviour
{
    protected Dictionary<Name, Sprite> enemyImageDictionary;
    protected Dictionary<Name, float> enemyScaleDictionary;
    protected Previewer previewerPrefab;

    [HideInInspector] public TextAsset fileToLoad;

    protected FileIO fileIO = new FileIO(new JsonParser(), ".txt");

    protected string fileName = "StageData";
    public string FileName { get { return fileName; } set { fileName = value; } }

    protected string fileLocation = "/Editor/Save/";
    public string FileLocation { get { return fileLocation; } set { fileLocation = value; } }

    public virtual void Initialize(
        Dictionary<Name, Sprite> enemyImageDictionary,
        Dictionary<Name, float> enemyScaleDictionary,
        Previewer previewerPrefab)
    {
        this.enemyImageDictionary = enemyImageDictionary;
        this.enemyScaleDictionary = enemyScaleDictionary;
        this.previewerPrefab = previewerPrefab;
    }

    public abstract void SaveData();
    public abstract void LoadData();

    protected void CreatePreviewer(SpawnData spawnData)
    {
        Previewer previewer = Instantiate(previewerPrefab, new Vector2(spawnData.spawnPosition.x, spawnData.spawnPosition.y), Quaternion.identity);
        previewer.ResetSprite(enemyImageDictionary[spawnData.name]);
        previewer.ResetScale(enemyScaleDictionary[spawnData.name]);
    }

    public void RemovePreviewer()
    {
        Previewer[] previewers = FindObjectsByType<Previewer>(FindObjectsSortMode.None);
        for (int i = 0; i < previewers.Length; i++)
        {
            DestroyImmediate(previewers[i].gameObject);
        }
    }
}
