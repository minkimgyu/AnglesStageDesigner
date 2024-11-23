using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct SerializableVector2
{
    [SerializeField] public float x, y;

    public SerializableVector2(Vector2 vector2)
    {
        this.x = vector2.x;
        this.y = vector2.y;
    }
}

public class FileIO
{
    JsonParser _parser;
    string _extension;

    public FileIO(JsonParser parser, string extension)
    {
        _parser = parser;
        _extension = extension;
        // Application.dataPath + filePath + fileName + _extension
    }

    string ReturnPath(string filePath, string fileName) 
    { return $"{Application.dataPath}/{filePath}/{fileName}{_extension}"; }

    public T LoadData<T>(string jData)
    {
        return _parser.JsonToObject<T>(jData);
    }

    public T LoadData<T>(string filePath, string fileName)
    {
        string path = ReturnPath(filePath, fileName);
        if (File.Exists(path) == false) return default;

        string jData = File.ReadAllText(path);
        return _parser.JsonToObject<T>(jData);
    }

    public void SaveData(object objectToParse, string filePath, string fileName)
    {
        string path;
        path = ReturnFilePath(filePath, fileName); // ��ġ�� �ʴ� �̸��� ã��

        Debug.Log(path);

        string jsonAsset = _parser.ObjectToJson(objectToParse);
        File.WriteAllText(path, jsonAsset); // �̷� ������� ����������
        AssetDatabase.Refresh();
    }

    string ReturnFilePath(string filePath, string fileName)
    {
        string path = ReturnPath(filePath, fileName);
        if (File.Exists(path) == false) return path;

        Debug.LogError("�̹� �ش� ��ο� ������ ������");

        string originName = fileName;
        int index = 0;

        do
        {
            if (index > 0) originName = $"{originName} {index}";

            path = ReturnPath(filePath, originName);
            if (File.Exists(path) == false) break; // �������� ������ break
            else index++;
        }
        while (true);

        return path;
    }
}
