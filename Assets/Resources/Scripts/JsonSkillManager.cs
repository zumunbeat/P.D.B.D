using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

// ���׸� JsonManager Ŭ����
public static class JsonManager
{
    // JSON ������ �ε��Ͽ� ������ Ÿ���� ��ü�� ��ȯ�ϴ� ���׸� �޼���
    public static T LoadJson<T>(string path)
    {
        //����  Class�̸� playerData = JsonManager.LoadJson<Class�̸�>(���);
        // JSON ���� �ε�
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile != null)
        {
            // JSON �Ľ�
            T data = JsonUtility.FromJson<T>(jsonFile.text);
            return data;
        }
        else
        {
            Debug.LogError("Failed to load JSON file at path: " + path);
            return default(T);
        }
    }
    public static void SaveJson<T>(T data, string path)
    {
        string json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + path + ".json", json);
        Debug.Log("Data saved to " + path);
    }
}

// ����ϴ� ������ Ŭ������
[System.Serializable]
public class Skill
{
    public string name;
    public int power;
    public int cost;
    public bool target;
}
[System.Serializable]

public class SkillArr
{
    public Skill[] skills;
}

[System.Serializable]
public class Stat
{
    public int health;
    public int strength;
}
