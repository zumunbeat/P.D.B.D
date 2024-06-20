using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

// 제네릭 JsonManager 클래스
public static class JsonManager
{
    // JSON 파일을 로드하여 지정된 타입의 객체로 변환하는 제네릭 메서드
    public static T LoadJson<T>(string path)
    {
        //사용법  Class이름 playerData = JsonManager.LoadJson<Class이름>(경로);
        // JSON 파일 로드
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile != null)
        {
            // JSON 파싱
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

// 사용하는 데이터 클래스들
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
