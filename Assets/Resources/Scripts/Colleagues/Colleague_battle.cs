using UnityEngine;

public class Colleague_battle : BaseCharacter
{
    private string skillpath = "JsonFiles/ColleagueSkill"; // 동료 스킬 경로
    private string statpath = "JsonFiles/ColleagueStat"; // 동료 스탯 경로
    public Stat collegueStat;
    [SerializeField]
    private GameObject ColleaguesButtonParent;
    public void Init()
    {
        skills = LoadData<SkillArr>(skillpath);
        collegueStat = LoadData<Stat>(statpath);
    }

    public T LoadData<T>(string path)
    {
        return JsonManager.LoadJson<T>(path);
    }

    public override void UseSkill(Skill skill)
    {
        Debug.Log("Colleague used skill: " + skill.name);
        // 스킬 사용 로직
    }

    public override void ActivateSkillButtons()
    {
        throw new System.NotImplementedException();
    }
}
