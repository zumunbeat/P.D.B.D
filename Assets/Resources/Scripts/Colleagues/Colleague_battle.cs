using UnityEngine;

public class Colleague_battle : BaseCharacter
{
    private string skillpath = "JsonFiles/ColleagueSkill"; // ���� ��ų ���
    private string statpath = "JsonFiles/ColleagueStat"; // ���� ���� ���
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

    public override void UseSkill(Skill skill, BaseCharacter target)
    {
        Debug.Log("Colleague used skill: " + skill.name+" to "+target.name);

        // ��ų ��� ����
    }

    public override void ActivateSkillButtons()
    {
        ColleaguesButtonParent.SetActive(true);
    }
}
