using static Interface;
using UnityEngine;

public class Enemy_battle : BaseCharacter
{
    private string skillpath = "JsonFiles/EnemySkill";
    private string statpath = "JsonFiles/EnemyStat";
    public Stat enemystat;

    public void Init()
    {
        skills = LoadData<SkillArr>(skillpath);
        enemystat = LoadData<Stat>(statpath);
    }

    public T LoadData<T>(string path)
    {
        return JsonManager.LoadJson<T>(path);
    }

    public void SaveData<T>(T data, string path)
    {
        JsonManager.SaveJson(data, path);
    }

    public override void UseSkill(Skill skill)
    {
        Debug.Log("Enemy used skill: " + skill.name);
        // 스킬 사용 로직
    }

    public override void ActivateSkillButtons()
    {
        throw new System.NotImplementedException();
    }
}